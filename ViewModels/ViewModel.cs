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
        private RelayCommand shiftRightCommand;
        private RelayCommand shiftUpCommand;
        private RelayCommand shiftDownCommand;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand ShiftLeftCommand => shiftLeftCommand;
        public ICommand ShiftRightCommand => shiftRightCommand;

        public ICommand ShiftUpCommand => shiftUpCommand;

        public ICommand ShiftDownCommand => shiftDownCommand;

        //public RelayCommand ShiftRightCommand { get; init; }
        //public RelayCommand ShiftUpCommand { get; init; }
        //public RelayCommand ShiftDownCommand { get; init; }


        public TableVM()
        {
            shiftLeftCommand = new RelayCommand(ShiftLeft);
            shiftRightCommand = new RelayCommand(ShiftRight);
            shiftDownCommand = new RelayCommand(ShiftDown);
            shiftUpCommand = new RelayCommand(ShiftUp);
            GenerateACell();
        }

        public void ShiftLeft()
        {
            bool shifted = false;
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                int index = 0;
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    if (cells[i, j] != null)    
                    {
                        if (index > 0 && cells[i, index - 1].num == cells[i, j].num)
                        {
                            cells[i, index - 1].num *= 2;
                            cells[i, j] = null;
                            shifted = true;
                        }
                        else
                        {
                            if (j != index)
                            {
                                cells[i, index] = cells[i, j];
                                cells[i, j] = null;
                                shifted = true;
                            }
                            index++;
                        }
                    }
                }
            }
            if (shifted)
            {
                GenerateACell();
            }

        }

        public void ShiftRight()
        {
            bool shifted = false;
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                int index = cells.GetLength(1) - 1;
                for (int j = cells.GetLength(1) - 1; j >= 0; j--)
                {
                    if (cells[i, j] != null)
                    {
                        if (index < cells.GetLength(1) - 1 && cells[i, index + 1].num == cells[i, j].num)
                        {
                            cells[i, index + 1].num *= 2;
                            cells[i, j] = null;
                            shifted = true;
                        }
                        else
                        {
                            if (j != index)
                            {
                                cells[i, index] = cells[i, j];
                                cells[i, j] = null;
                                shifted = true;
                            }
                            index--;
                        }
                    }
                }
            }
            if (shifted)
            {
                GenerateACell();
            }
        }

        public void ShiftDown()
        {
            bool shifted = false;
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                int index = cells.GetLength(0) - 1;
                for (int i = cells.GetLength(0) - 1; i >= 0; i--)
                {
                    if (cells[i, j] != null)
                    {
                        if (index < cells.GetLength(0) - 1 && cells[index + 1, j].num == cells[i, j].num)
                        {
                            cells[index + 1, j].num *= 2;
                            cells[i, j] = null;
                            shifted = true;
                        }
                        else
                        {
                            if (i != index)
                            {
                                cells[index, j] = cells[i, j];
                                cells[i, j] = null;
                                shifted = true;
                            }
                            index--;
                        }
                    }
                }
            }
            if (shifted)
            {
                GenerateACell();
            }
        }

        public void ShiftUp()
        {
            bool shifted = false;
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                int index = 0;
                for (int i = 0; i < cells.GetLength(0); i++)
                {
                    if (cells[i, j] != null)
                    {
                        if (index > 0 && cells[index - 1, j].num == cells[i, j].num)
                        {
                            cells[index - 1, j].num *= 2;
                            cells[i, j] = null;
                            shifted = true;
                        }
                        else
                        {
                            if (i != index)
                            {
                                cells[index, j] = cells[i, j];
                                cells[i, j] = null;
                                shifted = true;
                            }
                            index++;
                        }
                    }
                }
            }
            if (shifted)
            {
                GenerateACell();
            }
        }




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
