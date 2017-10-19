﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSACharacterSheet.FileReader.CombatInfo
{
    [Serializable]
    public class Weapon : BindableBase
    {
        #region Properties

        [XmlIgnore]
        private string _name;
        [XmlAttribute("Name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private int _handicap;
        [XmlAttribute("Handicap")]
        public int Handicap
        {
            get { return _handicap; }
            set
            {
                if (_handicap == value)
                    return;
                _handicap = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private WeaponType _type;
        [XmlAttribute("Type")]
        public WeaponType Type
        {
            get { return _type; }
            set
            {
                if (_type == value)
                    return;
                _type = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}
