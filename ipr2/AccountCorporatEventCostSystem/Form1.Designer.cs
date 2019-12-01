namespace AccountCorporatEventCostSystem
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.eventsListBox = new System.Windows.Forms.ListBox();
            this.nameField = new System.Windows.Forms.TextBox();
            this.visitorsAmountField = new System.Windows.Forms.TextBox();
            this.rentPriceField = new System.Windows.Forms.TextBox();
            this.costPerVisitorField = new System.Windows.Forms.TextBox();
            this.equipmentEntertainmentCostsField = new System.Windows.Forms.TextBox();
            this.startDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.visitorsAmountLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.costPerVisitorLabel = new System.Windows.Forms.Label();
            this.rentPriceLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.startDateLabel = new System.Windows.Forms.Label();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.sortButton = new System.Windows.Forms.Button();
            this.filterButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.sortTypeComboBox = new System.Windows.Forms.ComboBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.filterTypeComboBox = new System.Windows.Forms.ComboBox();
            this.sortFieldComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // eventsListBox
            // 
            this.eventsListBox.FormattingEnabled = true;
            this.eventsListBox.ItemHeight = 16;
            this.eventsListBox.Location = new System.Drawing.Point(402, 57);
            this.eventsListBox.Name = "eventsListBox";
            this.eventsListBox.Size = new System.Drawing.Size(508, 356);
            this.eventsListBox.TabIndex = 0;
            this.eventsListBox.SelectedIndexChanged += new System.EventHandler(this.selectedIndexChenged);
            // 
            // nameField
            // 
            this.nameField.Location = new System.Drawing.Point(12, 57);
            this.nameField.Name = "nameField";
            this.nameField.Size = new System.Drawing.Size(384, 22);
            this.nameField.TabIndex = 1;
            this.nameField.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateName);
            // 
            // visitorsAmountField
            // 
            this.visitorsAmountField.Location = new System.Drawing.Point(57, 134);
            this.visitorsAmountField.Name = "visitorsAmountField";
            this.visitorsAmountField.Size = new System.Drawing.Size(100, 22);
            this.visitorsAmountField.TabIndex = 2;
            this.visitorsAmountField.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateInt);
            // 
            // rentPriceField
            // 
            this.rentPriceField.Location = new System.Drawing.Point(151, 279);
            this.rentPriceField.Name = "rentPriceField";
            this.rentPriceField.Size = new System.Drawing.Size(100, 22);
            this.rentPriceField.TabIndex = 3;
            this.rentPriceField.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            // 
            // costPerVisitorField
            // 
            this.costPerVisitorField.Location = new System.Drawing.Point(247, 134);
            this.costPerVisitorField.Name = "costPerVisitorField";
            this.costPerVisitorField.Size = new System.Drawing.Size(100, 22);
            this.costPerVisitorField.TabIndex = 4;
            this.costPerVisitorField.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            // 
            // equipmentEntertainmentCostsField
            // 
            this.equipmentEntertainmentCostsField.Location = new System.Drawing.Point(151, 208);
            this.equipmentEntertainmentCostsField.Name = "equipmentEntertainmentCostsField";
            this.equipmentEntertainmentCostsField.Size = new System.Drawing.Size(100, 22);
            this.equipmentEntertainmentCostsField.TabIndex = 5;
            this.equipmentEntertainmentCostsField.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDouble);
            // 
            // startDateTimePicker
            // 
            this.startDateTimePicker.CustomFormat = "";
            this.startDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDateTimePicker.Location = new System.Drawing.Point(26, 351);
            this.startDateTimePicker.Name = "startDateTimePicker";
            this.startDateTimePicker.Size = new System.Drawing.Size(160, 22);
            this.startDateTimePicker.TabIndex = 6;
            this.startDateTimePicker.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateStartDate);
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.CustomFormat = "";
            this.endDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDateTimePicker.Location = new System.Drawing.Point(209, 351);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.Size = new System.Drawing.Size(160, 22);
            this.endDateTimePicker.TabIndex = 7;
            this.endDateTimePicker.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateEndDate);
            // 
            // visitorsAmountLabel
            // 
            this.visitorsAmountLabel.AutoSize = true;
            this.visitorsAmountLabel.Location = new System.Drawing.Point(57, 114);
            this.visitorsAmountLabel.Name = "visitorsAmountLabel";
            this.visitorsAmountLabel.Size = new System.Drawing.Size(106, 17);
            this.visitorsAmountLabel.TabIndex = 8;
            this.visitorsAmountLabel.Text = "Visitors Amount";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 37);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(45, 17);
            this.nameLabel.TabIndex = 9;
            this.nameLabel.Text = "Name";
            // 
            // costPerVisitorLabel
            // 
            this.costPerVisitorLabel.AutoSize = true;
            this.costPerVisitorLabel.Location = new System.Drawing.Point(244, 114);
            this.costPerVisitorLabel.Name = "costPerVisitorLabel";
            this.costPerVisitorLabel.Size = new System.Drawing.Size(105, 17);
            this.costPerVisitorLabel.TabIndex = 10;
            this.costPerVisitorLabel.Text = "Cost Per Visitor";
            // 
            // rentPriceLabel
            // 
            this.rentPriceLabel.AutoSize = true;
            this.rentPriceLabel.Location = new System.Drawing.Point(160, 259);
            this.rentPriceLabel.Name = "rentPriceLabel";
            this.rentPriceLabel.Size = new System.Drawing.Size(74, 17);
            this.rentPriceLabel.TabIndex = 11;
            this.rentPriceLabel.Text = "Rent Price";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Equipment and Entertainment Costs";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // startDateLabel
            // 
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Location = new System.Drawing.Point(66, 331);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(73, 17);
            this.startDateLabel.TabIndex = 13;
            this.startDateLabel.Text = "Start Time";
            this.startDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // endDateLabel
            // 
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Location = new System.Drawing.Point(256, 331);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Size = new System.Drawing.Size(68, 17);
            this.endDateLabel.TabIndex = 14;
            this.endDateLabel.Text = "End Time";
            this.endDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.AutoSize = true;
            this.HeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HeaderLabel.Location = new System.Drawing.Point(158, 12);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(93, 20);
            this.HeaderLabel.TabIndex = 15;
            this.HeaderLabel.Text = "Event Info";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(614, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Events List";
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(827, 419);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(83, 34);
            this.resetButton.TabIndex = 22;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // sortButton
            // 
            this.sortButton.Location = new System.Drawing.Point(546, 419);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(83, 34);
            this.sortButton.TabIndex = 23;
            this.sortButton.Text = "Sort";
            this.sortButton.UseVisualStyleBackColor = true;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(402, 419);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(83, 34);
            this.filterButton.TabIndex = 24;
            this.filterButton.Text = "Filter";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(12, 419);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(83, 34);
            this.addButton.TabIndex = 25;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(101, 419);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(83, 34);
            this.updateButton.TabIndex = 26;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(190, 419);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(83, 34);
            this.deleteButton.TabIndex = 27;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // sortTypeComboBox
            // 
            this.sortTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortTypeComboBox.ItemHeight = 16;
            this.sortTypeComboBox.Items.AddRange(new object[] {
            "Asc",
            "Desc"});
            this.sortTypeComboBox.Location = new System.Drawing.Point(756, 425);
            this.sortTypeComboBox.Name = "sortTypeComboBox";
            this.sortTypeComboBox.Size = new System.Drawing.Size(65, 24);
            this.sortTypeComboBox.TabIndex = 28;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(295, 419);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(83, 34);
            this.clearButton.TabIndex = 29;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // filterTypeComboBox
            // 
            this.filterTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterTypeComboBox.ItemHeight = 16;
            this.filterTypeComboBox.Items.AddRange(new object[] {
            "=",
            ">",
            "<",
            ">=",
            "<=",
            "!="});
            this.filterTypeComboBox.Location = new System.Drawing.Point(491, 425);
            this.filterTypeComboBox.Name = "filterTypeComboBox";
            this.filterTypeComboBox.Size = new System.Drawing.Size(49, 24);
            this.filterTypeComboBox.TabIndex = 30;
            // 
            // sortFieldComboBox
            // 
            this.sortFieldComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortFieldComboBox.ItemHeight = 16;
            this.sortFieldComboBox.Items.AddRange(new object[] {
            "ID",
            "Name",
            "VisitorsAmount",
            "CostPerVisitor",
            "EquipmentEntertainmentCosts",
            "RentPrice",
            "StartTime",
            "EndTime"});
            this.sortFieldComboBox.Location = new System.Drawing.Point(635, 425);
            this.sortFieldComboBox.Name = "sortFieldComboBox";
            this.sortFieldComboBox.Size = new System.Drawing.Size(115, 24);
            this.sortFieldComboBox.TabIndex = 31;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 463);
            this.Controls.Add(this.sortFieldComboBox);
            this.Controls.Add(this.filterTypeComboBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.sortTypeComboBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.sortButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HeaderLabel);
            this.Controls.Add(this.endDateLabel);
            this.Controls.Add(this.startDateLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rentPriceLabel);
            this.Controls.Add(this.costPerVisitorLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.visitorsAmountLabel);
            this.Controls.Add(this.endDateTimePicker);
            this.Controls.Add(this.startDateTimePicker);
            this.Controls.Add(this.equipmentEntertainmentCostsField);
            this.Controls.Add(this.costPerVisitorField);
            this.Controls.Add(this.rentPriceField);
            this.Controls.Add(this.visitorsAmountField);
            this.Controls.Add(this.nameField);
            this.Controls.Add(this.eventsListBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Corporate Event Managment System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Close);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox eventsListBox;
        private System.Windows.Forms.TextBox nameField;
        private System.Windows.Forms.TextBox visitorsAmountField;
        private System.Windows.Forms.TextBox rentPriceField;
        private System.Windows.Forms.TextBox costPerVisitorField;
        private System.Windows.Forms.TextBox equipmentEntertainmentCostsField;
        private System.Windows.Forms.DateTimePicker startDateTimePicker;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.Label visitorsAmountLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label costPerVisitorLabel;
        private System.Windows.Forms.Label rentPriceLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label startDateLabel;
        private System.Windows.Forms.Label endDateLabel;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button sortButton;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ComboBox sortTypeComboBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.ComboBox filterTypeComboBox;
        private System.Windows.Forms.ComboBox sortFieldComboBox;
    }
}

