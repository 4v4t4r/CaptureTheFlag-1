using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Models
{
    public class GameMap : PropertyChangedBase
    {
        #region Private properties
        public string url { get; set; } //url for current resource
        private string _name { get; set; } //(required=True, max_length=100)
        private string _description; //(null=True, max_length=255)
        private float _radius;
        public string author { get; set; } //url for user object
        private float _lat;
        private float _lon;
        public List<string> games { get; set; } //list of urls to games objects
        #endregion

        #region Observable properties
        public string name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyOfPropertyChange(() => name);
                }
            }
        }

        public string description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    NotifyOfPropertyChange(() => description);
                }
            }
        }

        public float radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    NotifyOfPropertyChange(() => radius);
                }
            }
        }

        public float lat
        {
            get { return _lat; }
            set
            {
                if (_lat != value)
                {
                    _lat = value;
                    NotifyOfPropertyChange(() => lat);
                }
            }
        }

        public float lon
        {
            get { return _lon; }
            set
            {
                if (_lon != value)
                {
                    _lon = value;
                    NotifyOfPropertyChange(() => lon);
                }
            }
        }
        #endregion
    }
}
