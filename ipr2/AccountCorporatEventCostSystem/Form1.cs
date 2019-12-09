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
        }; // для удобства в программе записал сюда названия стобцов таблицы из Access
        private readonly string dateTimeFormat = "HH:mm dd.MM.yyyy";    // формат даты 
        private OdbcConnection dbConnection;    // объект подключения к базе данных
        private int itemId;                     // id текущего элемента
        private string filterCommand;           // сюда будет записывать дополнительная команда фильтрации (если надо фильтровать)
        private List<Tuple<string, string>> errors; // список для хранения названия поля(где ошибка) и текст ошибки

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
                                                                // выше просто выставляю значения графических элементов по умолчанию

            dbConnection = new OdbcConnection(Program.ConnectionString);    // подключение к базе данных Program.ConnectionString мможешь посмотреть в файле Program.cs
                                                                            // там просто путь к файлу с базой данных и специально отформатированная строка для подключения
            dbConnection.Open();    // открываем подключение
            Update("ID");   // обновляем список записей (в программе, т.е. список при старте программы пустой, но почти моментально заполняется данными из таблицы)
        }

        private void Close(object sender, FormClosingEventArgs e)   // при закрытии программы
        {
            dbConnection.Close();   // закрываем подключение к базе данных
        }

        private void setFields(object[] values) // функция для заполнения полей (которые слева и где подробная информация)
        {
            itemId = int.Parse(values[0].ToString());   // преобразовываем объект в строку, а потом в число
            nameField.Text = values[1].ToString();      // преобразовываем объект в строку
            visitorsAmountField.Text = values[2].ToString();    // преобразовываем объект в строку
            costPerVisitorField.Text = values[3].ToString();    // преобразовываем объект в строку
            rentPriceField.Text = values[4].ToString(); // преобразовываем объект в строку
            equipmentEntertainmentCostsField.Text = values[5].ToString();   // преобразовываем объект в строку
            try
            {
                startDateTimePicker.CustomFormat = dateTimeFormat;  // устанавливаем формат поля с датой
                startDateTimePicker.Value = (DateTime)values[6];    // пытаемся записать туда значение (может не получится) и тогда попадём в catch
                endDateTimePicker.CustomFormat = dateTimeFormat;    // аналогично
                endDateTimePicker.Value = (DateTime)values[7];
            }
            catch (Exception ex)    // сюда попадаем в случае если нажали на кнопку clear или если формат даты в таблице неправильный
            {
                if (ex is FormatException || ex is InvalidCastException)    // если определённые ошибки, то ...
                {
                    startDateTimePicker.CustomFormat = " "; // устанавливаем пустой формат
                    endDateTimePicker.CustomFormat = " ";
                    startDateTimePicker.Text = values[6].ToString();    // записываем значение
                    endDateTimePicker.Text = values[7].ToString();
                    return;
                }
                throw;
            }
        }

        private bool isAnyFieldEmpty()  // проверка на то есть ли хоть одно пустое поле
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

        private void Update(string field, int ix = 0, bool isFiltered = false)  // обновление списка с записями field поле по которому сортируем
                                                                                // ix индекс элемента, который хотим выбрать
                                                                                // isFiltered флаг надо ли фильровать
        {
            OdbcCommand command = dbConnection.CreateCommand(); // создание ODBC комманды
            if (!isFiltered)    
                // записываем команду
                // {string.Join(", ", dbFields)} - соеденияет все элементы коллекции dbFields в строку через ", "
                // и результат будет вписан внутрь строки, т.е. получится "SELECT ID, Name, VisitorsAmount, ... FROM [events] ...
                // {field} - просто подставит значение переменной в строку, т.е. получится что-то наподобие ... ORDER BY ID ... (если field равен строке "ID")
                // {sortTypeComboBox.Text.ToUpper()} - берёт ыбранное значение из списка возвможных соритровок (это такая выпдающая менюшка, с значениями Asc и Desc) и приводит его к виду ASC или DESC
                command.CommandText = $"SELECT {string.Join(", ", dbFields)} FROM [events] ORDER BY {field} {sortTypeComboBox.Text.ToUpper()}";
            else
                // {filterCommand} - если выбрана опция фильтрации вставляет значение переменной в строку
                // значение этой переменной задётся в другой функции ниже может быть таким например: 
                // "WHERE VisitorsAmount>10 AND Name=John AND ..."
                command.CommandText = $"SELECT {string.Join(", ", dbFields)} FROM [events] {filterCommand} ORDER BY {field} {sortTypeComboBox.Text.ToUpper()}";
            OdbcDataReader reader = command.ExecuteReader();    // выполняем команду
            eventsListBox.Items.Clear();    // очищаем список элементов

            object[] fieldValues = new object[8];   // массив куда будем записывать значения полученные из Access
            int i = 0;
            while (reader.Read())   // считывает 1 строку табилцы
            {
                reader.GetValues(fieldValues);  // запоминаем значения в массив
                eventsListBox.Items.Add(string.Join(" ",                
                fieldValues.Select(v =>                                 // для каждого значения из fieldValues проверяем тип данных
                                                                        // и если дата то конвертирем в строку с применением формата даты (записанного выше)
                                                                        // иначе просто преобразовываем в строку
                {
                    if (v.GetType() == typeof(DateTime))
                        return ((DateTime)v).ToString(dateTimeFormat);
                    return v.ToString();
                }).ToArray()));                                         // все значения преобразовываем в массив и массив соедениям в строку через пробел (строка 119: string.Join(" ", ...)
                if (i++ == 0)
                    setFields(fieldValues);     // вызывается только для первой записи в таблице (чтобы заполнить подробную информцию элемента)
            }

            eventsListBox.SetSelected(ix, true);    // выделяем элемент из списка записей

            reader.Close(); // закрываем объект для чтения данных из таблицы Access
        }

        private void resetButton_Click(object sender, EventArgs e)  // вызывается когда нажимаем сброс и сбрасывае все фильтры и сортировки
        {
            sortTypeComboBox.SelectedIndex = 0;
            sortFieldComboBox.SelectedIndex = 0;

            filterTypeComboBox.SelectedIndex = -1;
            Update(sortFieldComboBox.Text);
        }

        private void sortButton_Click(object sender, EventArgs e)   // вызывается когда нажимаем кнопку сортировки
        {
            Update(sortFieldComboBox.Text); // sortFieldComboBox.Text - выбранное поле в выпадающем списке
        }

        private void filterButton_Click(object sender, EventArgs e) // вызывается когда нажали кнопку фильтрации
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

        private void clearButton_Click(object sender, EventArgs e)  // вызывается когда нажали кнопку очистки
        {
            object[] values = { -1, "", "", "", "", "", "", "" };   
            setFields(values);
        }

        private bool isFormValid(object[] fields = null)    // проверка полей формы вызывается при добавлении, реактировании, фильтрации
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

        private void deleteButton_Click(object sender, EventArgs e) // вызывается при нажатии кнопки удалить
        {
            OdbcCommand command = dbConnection.CreateCommand();
            command.CommandText = $"DELETE FROM [events] WHERE [ID] = {itemId}";    // команда удаления
            command.ExecuteNonQuery();
            var ix = eventsListBox.SelectedIndex - 1;   // запоминаем предыдущий индекс выбранного элемента
            Update("ID", ix >= 0 ? ix : 0); // обновляем и передаём значени ix или 0 (если ix < 0, т.е. элемент, который мы удалил был первым)
        }

        private void updateButton_Click(object sender, EventArgs e) // вызвыается при нажатии кнопки обновить
        {
            if (!isFormValid()) // проверка правильности нет неправильно заполненных полей
                return;
            OdbcCommand command = dbConnection.CreateCommand();
            command.CommandText = $"UPDATE [events] SET [Name] = \'{nameField.Text}\', [VisitorsAmount] = {visitorsAmountField.Text}, [CostPerVisitor] = {costPerVisitorField.Text}, [RentPrice] = {rentPriceField.Text}, [EquipmentEntertainmentCosts] = {equipmentEntertainmentCostsField.Text}, [StartTime] = {startDateTimePicker.Value.ToOADate()}, [EndTime] = {endDateTimePicker.Value.ToOADate()} WHERE [ID] = {itemId}";
            command.ExecuteNonQuery();
            Update(sortFieldComboBox.Text, eventsListBox.SelectedIndex);
        }

        private void addButton_Click(object sender, EventArgs e)    // вызывается при нажатии кнопки добавить
        {
            if (isAnyFieldEmpty())  // проверка нет ли пустых полей
            {
                MessageBox.Show("Fill all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!isFormValid()) // провекра правильности полей
                return;

            OdbcCommand command = dbConnection.CreateCommand();
            // dbFields.Skip(1).ToArray() - вернёт все массив dbFields без первого значения, т.е. без ID
            // {string.Join(", ", dbFields.Skip(1).ToArray())} - соеденит все элементы массива через запятую в строку
            command.CommandText = $"INSERT INTO [events] ({string.Join(", ", dbFields.Skip(1).ToArray())}) VALUES (\'{nameField.Text}\', {visitorsAmountField.Text}, {costPerVisitorField.Text}, {rentPriceField.Text}, {equipmentEntertainmentCostsField.Text}, {startDateTimePicker.Value.ToOADate()}, {endDateTimePicker.Value.ToOADate()})";
            command.ExecuteNonQuery();
            Update(sortFieldComboBox.Text, eventsListBox.Items.Count);
        }

        private void selectedIndexChenged(object sender, EventArgs e)   //  вызывается при изменении выбранного элемента в списке
                                                                        //  обновляет значения в полях (обновляет подробную информцию)
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
        // дальше идут раздичного рода проверки, которые вызывается в функции isFormValid, т.к. все значения полей в графическом интерфейсе это изначально строки
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
