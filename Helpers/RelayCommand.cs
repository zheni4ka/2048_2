using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2048.Helpers
{
  
        public class RelayCommand : BaseCommand
        {
            private readonly Action execute;

            public RelayCommand(Action execute)
            {
                this.execute = execute;
            }

            public override void Execute(object parameter)
            {
                execute.Invoke();
            }

    }
    
}
