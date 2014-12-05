using be.belgium.eid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace nmct.ba.cashlessproject.ba.kassa.klant.idreader
{
    public class ReadId
    {
        public string Name { get; set; }
        public string Adres { get; set; }
        public Byte[] Bytes { get; set; }

        public static ReadId Lees()
        {
            try
            {
                BEID_ReaderSet.initSDK();
                BEID_ReaderSet ReaderSet;
                ReaderSet = BEID_ReaderSet.instance();

                BEID_ReaderContext Reader;
                Reader = ReaderSet.getReader();

                if (Reader.isCardPresent())
                {
                    ReadId id = Load_eid(Reader);
                    BEID_ReaderSet.releaseSDK();
                    return id;
                }
                return null;           

            }
            catch (Exception ex)
            {
                BEID_ReaderSet.releaseSDK();
                return null;  
            }

        }
        private static ReadId Load_eid(BEID_ReaderContext Reader)
        {
            string Naam, Adres;
            
            BEID_EIDCard card;
            card = Reader.getEIDCard();
            if (card.isTestCard())
            {
                card.setAllowTestCard(true);
            }

            BEID_EId doc;
            doc = card.getID();

            Naam = doc.getFirstName() + " " + doc.getSurname();
            Adres = doc.getStreet() + " " + doc.getZipCode() + " " + doc.getMunicipality();
            /*sText = "";
            sText += "PERSONAL DATA" + "\r\n";
            sText += "\r\n";
            sText += "First Name = " + doc.getFirstName() + "\r\n";
            sText += "Last Name = " + doc.getSurname() + "\r\n";
            sText += "Gender = " + doc.getGender() + "\r\n";
            sText += "DateOfBirth = " + doc.getDateOfBirth() + "\r\n";
            sText += "LocationOfBirth = " + doc.getLocationOfBirth() + "\r\n";
            sText += "Nobility = " + doc.getNobility() + "\r\n";
            sText += "Nationality = " + doc.getNationality() + "\r\n";
            sText += "NationalNumber = " + doc.getNationalNumber() + "\r\n";
            sText += "SpecialOrganization = " + doc.getSpecialOrganization() + "\r\n";
            sText += "MemberOfFamily = " + doc.getMemberOfFamily() + "\r\n";
            sText += "AddressVersion = " + doc.getAddressVersion() + "\r\n";
            sText += "Street = " + doc.getStreet() + "\r\n";
            sText += "ZipCode = " + doc.getZipCode() + "\r\n";
            sText += "Municipality = " + doc.getMunicipality() + "\r\n";
            sText += "Country = " + doc.getCountry() + "\r\n";
            sText += "SpecialStatus = " + doc.getSpecialStatus() + "\r\n";

            sText += "\r\n";
            sText += "\r\n";

            sText += @"CARD DATA" + "\r\n";
            sText += "\r\n";
            sText += "LogicalNumber = " + doc.getLogicalNumber() + "\r\n";
            sText += "ChipNumber = " + doc.getChipNumber() + "\r\n";
            sText += "ValidityBeginDate = " + doc.getValidityBeginDate() + "\r\n";
            sText += "ValidityEndDate = " + doc.getValidityEndDate() + "\r\n";
            sText += "IssuingMunicipality = " + doc.getIssuingMunicipality() + "\r\n";*/

            BEID_Picture picture;
            picture = card.getPicture();
            byte[] bytearray;
            bytearray = picture.getData().GetBytes();

            return new ReadId() { Name = Naam, Adres = Adres, Bytes = bytearray };

        }
    }
}
