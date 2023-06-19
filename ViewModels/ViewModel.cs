using _2048.Helpers;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2048.ViewModels
{
    //[AddINotifyPropertyChangedInterface]
    public class TableVM : INotifyPropertyChanged
    {
   
        private CellVM[,] cells = new CellVM[4, 4];
        public CellVM[] Cells => cells.Cast<CellVM>().ToArray();

        private int Fst_Num = 2; private int Lst_Num = 2048;

        private RelayCommand shiftLeftCommand;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand ShiftLeftCommand => shiftLeftCommand;

        //public RelayCommand ShiftRightCommand { get; init; }
        //public RelayCommand ShiftUpCommand { get; init; }
        //public RelayCommand ShiftDownCommand { get; init; }


        public TableVM()
        {
            shiftLeftCommand = new RelayCommand(ShiftLeft);
            GenerateACell();
            GenerateACell();
        }

        public void ShiftLeft() {GenerateACell();}

       
        private void GenerateACell()
        {
            int row, col;
            do
            {
                row = new Random().Next(4);
                col = new Random().Next(4);
            } while (cells[row, col] != null);
            cells[row, col] = new CellVM(2);

            OnPropertyChanged("Cells");
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }





    [AddINotifyPropertyChangedInterface]
    public class CellVM
    {
        private readonly Dictionary<int, SolidColorBrush> NumAndColorPairs = new()
        {
            {2, Brushes.Cornsilk},
            {4, Brushes.Moccasin},
            {8, Brushes.LightSalmon},
            {16, Brushes.Coral},
            {32, Brushes.Salmon},
            {64, Brushes.Red},
            {128, Brushes.Khaki},
            {256, Brushes.Khaki},
            {512, Brushes.PaleGoldenrod},
            {1024, Brushes.PaleGoldenrod},
            {2048, Brushes.Gold}
        };

        public int num { get; set; }
        
        public SolidColorBrush brush => NumAndColorPairs[num];

        public CellVM(int number) { num = number;}

    }



}
