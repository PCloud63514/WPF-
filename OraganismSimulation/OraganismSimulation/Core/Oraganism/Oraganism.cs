using GalaSoft.MvvmLight;

namespace OraganismSimulation.Oraganism.Core
{
    public class Oraganism:ObservableObject
    {
        /// <summary>
        /// JSON Format
        /// 
        ///{
	    ///"List":
        ///    [
	    ///	    {
	    ///		    "X": "0",
	    ///		    "Y": "0",
	    ///		    "Width: "0",
	    ///		    "Height: "0",
	    ///		    "Value: "0"
        ///     }
	    ///    ]
        ///}
        /// </summary>
        #region private Field
        private double _x;
        private double _y;
        private double _width;
        private double _height;
        private double _value;
        #endregion
        #region Field
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                RaisePropertyChanged(nameof(X));
            }
        }
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                RaisePropertyChanged(nameof(Width));
            }
        }
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                RaisePropertyChanged(nameof(Height));
            }
        }
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }
        #endregion

        public Oraganism()
        {

        }

        public Oraganism(OraganismInfo oraganismInfo)
        {
            X = oraganismInfo.X;
            Y = oraganismInfo.Y;
            Width = oraganismInfo.Width;
            Height = oraganismInfo.Height;
            Value = oraganismInfo.Value;
        }
    }
}
