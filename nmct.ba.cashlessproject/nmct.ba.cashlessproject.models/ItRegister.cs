using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.models
{
    public class ItRegister
    {
        #region property's
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _registerName;

        [Required(ErrorMessage="Vul een Kassa naam in")]
        [DisplayName("KassaNaam")]
        public string RegisterName
        {
            get { return _registerName; }
            set { _registerName = value; }
        }
        private string _device;

        [Required(ErrorMessage="Vul een kassa in")]
        [DisplayName("Kassa")]
        public string Device
        {
            get { return _device; }
            set { _device = value; }
        } 
        private int _purchaseDate;

        [Range(1, int.MaxValue, ErrorMessage = "Vul een Aankoopdatum in")]
        //[MinLength(4)]
        [DisplayName("AankoopDatum")]
        public int PurchaseDate
        {
            get { return _purchaseDate; }
            set { _purchaseDate = value; }
        }

        private int _expireDate;
        [Range(1, int.MaxValue, ErrorMessage = "Vul een VervalDatum in")]
        //[MinLength(4)]
        [DisplayName("VervalDatum")]
        public int ExpireDate
        {
            get { return _expireDate; }
            set { _expireDate = value; }
        }
        #endregion
    }
}
