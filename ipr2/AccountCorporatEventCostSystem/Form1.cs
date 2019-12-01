using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AccountCorporatEventCostSystem
{
    public partial class Form1 : Form
    {
        private readonly string[] dbFields =
        {
            "ID", 
            "Name", 
            "VisitorsAmount", 
            "CostPerVisitor", 
            "RentPrice", 
            "EquipmentEntertainmentCosts", 
            "StartTime", 
            "EndTime"
        };
        private readonly string dateTimeFormat = "HH:mm dd.MM.yyyy";
        private OdbcConnection dbConnection;
        private int itemId;
        private string filterCommand;
        private List<Tuple<string, string>> errors;

        public Form1()
        {
            InitializeComponent();
            AutoValidate = AutoValidate.Disable;
            errors = new List<Tuple<string, string>>();
            startDateTimePicker.CustomFormat = dateTimeFormat;
            endDateTimePicker.CustomFormat = dateTimeFormat;
            sortTypeComboBox.SelectedIndex = 0;
            sortFieldComboBox.SelectedIndex = 0;
            filterTypeComboBox.SelectedIndex = -1;

            dbConnection = new OdbcConnection(Program.ConnectionString);
            dbConnection.Open();
            Update("ID");
        }

        private void Close(object sender, FormClosingEventArgs e)
        {
            dbConnection.Close();
        }

        private void setFields(object[] values)
        {
            itemId = int.Parse(values[0].ToString());
            nameField.Text = values[1].ToString();
            visitorsAmountField.Text = values[2].ToString();
            costPerVisitorField.Text = values[3].ToString();
            rentPriceField.Text = values[4].ToString();
            equipmentEntertainmentCostsField.Text = values[5].ToString();
            try
            {
                startDateTimePicker.CustomFormat = dateTimeFormat;
                startDateTimePicker.Value = (DateTime)values[6];
                endDateTimePicker.CustomFormat = dateTimeFormat;
                endDateTimePicker.Value = (DateTime)values[7];
            }
            catch (Exception ex)
            {
                if (ex is FormatException || ex is InvalidCastException)
                {
                    startDateTimePicker.CustomFormat = " ";
                    endDateTimePicker.CustomFormat = " ";
                    startDateTimePicker.Text = values[6].ToString();
                    endDateTimePicker.Text = values[7].ToString();
                    return;
                }
                throw;
            }
        }

        private bool isAnyFieldEmpty()
        {
            string[] values = {
                nameField.Text,
                visitorsAmountField.Text,
                costPerVisitorField.Text,
                rentPriceField.Text,
                equipmentEntertainmentCostsField.Text,
                startDateTimePicker.Text,
                endDateTimePicker.Text
            };
            return values.Any(v => v == "" || v == " ");
        }

        private void Update(string field, int ix = 0, bool isFiltered = false)
        {
            OdbcCommand command = dbConnection.CreateCommand();
            if (!isFiltered)
                command.CommandText = $"SELECT {string.Join(", ", dbFields)} FROM [events] ORDER BY {field} {sortTypeComboBox.Text.ToUpper()}";
            else
                command.CommandText = $"SELECT {string.Join(", ", dbFields)} FROM [events] {filterCommand} ORDER BY {field} {sortTypeComboBox.Text.ToUpper()}";
            OdbcDataReader reader = command.ExecuteReader();
            eventsListBox.Items.Clear();

            object[] fieldValues = new object[8];
            int i = 0;
            while (reader.Read())
            {
                reader.GetValues(fieldValues);
                eventsListBox.Items.Add(string.Join(" ", fieldValues.Select(v =>
                {
                    if (v.GetType() == typeof(DateTime))
                        return ((DateTime)v).ToString(dateTimeFormat);
                    return v.ToString();
                }).ToArray()));
                if (i++ == 0)
                    setFields(fieldValues);
            }

            eventsListBox.SetSelected(ix, true);

            reader.Close();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            sortTypeComboBox.SelectedIndex = 0;
            sortFieldComboBox.SelectedIndex = 0;

            filterTypeComboBox.SelectedIndex = -1;
            Update(sortFieldComboBox.Text);
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            Update(sortFieldComboBox.Text);
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            Tuple<object, string>[] values = new Tuple<object, string>[]{
                new Tuple<object, string>(nameField, dbFields[1]),
                new Tuple<object, string>(visitorsAmountField, dbFields[2]),
                new Tuple<object, string>(costPerVisitorField, dbFields[3]),
                new Tuple<object, string>(equipmentEntertainmentCostsField, dbFields[4]),
                new Tuple<object, string>(rentPriceField, dbFields[5])
            };
            Tuple<object, string>[] dates = new Tuple<object, string>[]
            {
                new Tuple<object, string>(startDateTimePicker, dbFields[6]),
                new Tuple<object, string>(endDateTimePicker, dbFields[7])
            };

            var filterFields = values.Where(v => (v.Item1 as TextBox)?.Text.Length != 0)
                                     .Concat(dates.Where(v => (v.Item1 as DateTimePicker)?.Text.Length != 1));
            if (!isFormValid(values.Concat(dates).Except(filterFields).Select(v => v.Item1).ToArray()))
                return;
            var filteredResultFields = filterFields
                .Select(v => $"[{v.Item2}]{filterTypeComboBox.SelectedItem}{(v.Item1 as TextBox)?.Text ?? (v.Item1 as DateTimePicker).Value.ToOADate().ToString()}");
            filterCommand = "WHERE " + string.Join($" AND ", filteredResultFields);
            Update(sortFieldComboBox.Text, isFiltered: true);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            object[] values = { -1, "", "", "", "", "", "", "" };
            setFields(values);
        }

        private bool isFormValid(object[] fields = null)
        {
            errors.Clear();
            if ((fields?.Length ?? 0) != 0)
            {
                fields.ToList().ForEach(f => {
                    var t = (f as TextBox);
                    if (t != null)
                        t.Enabled = false;
                    else
                        (f as DateTimePicker).Enabled = false;
                });
                ValidateChildren(ValidationConstraints.Enabled);
                fields.ToList().ForEach(f => {
                    var t = (f as TextBox);
                    if (t != null)
                        t.Enabled = true;
                    else
                        (f as DateTimePicker).Enabled = true;
                });
            }
            else
                ValidateChildren();
            if (errors.Count != 0)
            {
                MessageBox.Show(string.Join("\n", errors.Select(v => $"{v.Item1} - {v.Item2}")), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            OdbcCommand command = dbConnection.CreateCommand();
            command.CommandText = $"DELETE FROM [events] WHERE [ID] = {itemId}";
            command.ExecuteNonQuery();
            var ix = eventsListBox.SelectedIndex - 1;
            Update("ID", ix >= 0 ? ix : 0);
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (!isFormValid())
                return;
            OdbcCommand command = dbConnection.CreateCommand();
            command.CommandText = $"UPDATE [events] SET [Name] = \'{nameField.Text}\', [VisitorsAmount] = {visitorsAmountField.Text}, [CostPerVisitor] = {costPerVisitorField.Text}, [RentPrice] = {rentPriceField.Text}, [EquipmentEntertainmentCosts] = {equipmentEntertainmentCostsField.Text}, [StartTime] = {startDateTimePicker.Value.ToOADate()}, [EndTime] = {endDateTimePicker.Value.ToOADate()} WHERE [ID] = {itemId}";
            command.ExecuteNonQuery();
            Update(sortFieldComboBox.Text, eventsListBox.SelectedIndex);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (isAnyFieldEmpty())
            {
                MessageBox.Show("Fill all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!isFormValid())
                return;

            OdbcCommand command = dbConnection.CreateCommand();
            command.CommandText = $"INSERT INTO [events] ({string.Join(", ", dbFields.Skip(1).ToArray())}) VALUES (\'{nameField.Text}\', {visitorsAmountField.Text}, {costPerVisitorField.Text}, {rentPriceField.Text}, {equipmentEntertainmentCostsField.Text}, {startDateTimePicker.Value.ToOADate()}, {endDateTimePicker.Value.ToOADate()})";
            command.ExecuteNonQuery();
            Update(sortFieldComboBox.Text, eventsListBox.Items.Count);
        }

        private void selectedIndexChenged(object sender, EventArgs e)
        {
            try
            {
                string id = eventsListBox.SelectedItem.ToString().Split(' ')[0];

                OdbcCommand command = dbConnection.CreateCommand();
                command.CommandText = $"SELECT {string.Join(", ", dbFields)} FROM [events] WHERE [ID]={id}";
                OdbcDataReader reader = command.ExecuteReader();

                reader.Read();

                object[] values = new object[8];
                reader.GetValues(values);

                setFields(values);
            }
            catch {}
        }

        private void ValidateName(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var target = (sender as TextBox);
            if (!Regex.IsMatch(target?.Text, @"^[\w -]+$"))
            {
                errors.Add(new Tuple<string, string>(target.Name, "Name can contain only letters, spaces and character '-'!"));
                e.Cancel = true;
            }
        }

        private void ValidateInt(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var target = (sender as TextBox);
            if (!Regex.IsMatch(target?.Text, @"^\d+$"))
            {
                errors.Add(new Tuple<string,string>(target.Name, "Enter valid integer value!"));
                e.Cancel = true;
            }
        }

        private void ValidateDouble(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var target = (sender as TextBox);
            if (!Regex.IsMatch(target?.Text, @"^\d+\.?\d+$"))
            {
                errors.Add(new Tuple<string,string>(target.Name, "Enter valid double value!"));
                e.Cancel = true;
            }
        }

        private void ValidateStartDate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var target = (sender as DateTimePicker);
            if (target?.Value >= endDateTimePicker.Value)
            {
                errors.Add(new Tuple<string, string>(target.Name, "Start date should be < then End date!"));
                e.Cancel = true;
            }
        }

        private void ValidateEndDate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var target = (sender as DateTimePicker);
            if (target?.Value <= startDateTimePicker.Value)
            {
                errors.Add(new Tuple<string,string>(target.Name, "End date should be > then Start date!"));
                e.Cancel = true;
            }
        }
    }
}
