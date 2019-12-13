using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace IB150218_MOBILE.ViewModel
{
    public class VrsteProizvoda
    {

        public int VrstaID { get; set; }
        public string Naziv { get; set; }

    }

    public class RootModel1 : INotifyPropertyChanged
    {

        List<VrsteProizvoda> vrsteList;
        public List<VrsteProizvoda> VrsteList
        {
            get { return vrsteList; }
            set
            {
                if (vrsteList != value)
                {
                    vrsteList = value;
                    OnPropertyChanged1();
                }
            }
        }

        VrsteProizvoda selectedvrste;
        public VrsteProizvoda SelectedVrste
        {
            get { return selectedvrste; }
            set
            {
                if (selectedvrste != value)
                {
                    selectedvrste = value;
                    OnPropertyChanged1();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged1([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}