using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsApplication341
{
    public partial class Form1 : Form
    {
        private Data data;

        public Form1()
        {
            InitializeComponent();

            data = new Data();

            //создаем контролы
            var nud1 = new NumericUpDown { Parent = this };
            var nud2 = new NumericUpDown { Parent = this, Left = 150 };
            var nud3 = new NumericUpDown { Parent = this, Left = 300 };
            var bt = new Button {Parent = this, Top = 100};

            //делаем привязки
            nud1.DataBindings.Add("Value", data, "Health", false, DataSourceUpdateMode.OnPropertyChanged);
            nud2.DataBindings.Add("Value", data, "Pits", false, DataSourceUpdateMode.OnPropertyChanged);
            nud3.DataBindings.Add("Value", data, "Unit", false, DataSourceUpdateMode.OnPropertyChanged);
            bt.DataBindings.Add("Text", data, "TotalHealth");
        }
    }

    class Data : INotifyPropertyChanged
    {
        //Свойства с поддержкой байндинга
        private decimal pits = 1;
        public decimal Pits{ get { return pits; } set { pits = value; OnChanged();} }

        private decimal health = 1;
        public decimal Health { get { return health; } set { health = value; OnChanged(); } }

        private decimal unit = 1;
        public decimal Unit { get { return unit; } set { unit = value; OnChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //Вычисялемое свойство
        public decimal MaxHealth
        {
            get { return Pits * 35; }
        }

        //Вычисялемое свойство
        public decimal TotalHealth
        {
            get { return Health * Unit; }
        }

        protected virtual void OnChanged()
        {
            //сигнализируем о том, что наши вычисляемые свойства поменялись
            PropertyChanged(this, new PropertyChangedEventArgs("MaxHealth"));
            PropertyChanged(this, new PropertyChangedEventArgs("TotalHealth"));
        }
    }
}
