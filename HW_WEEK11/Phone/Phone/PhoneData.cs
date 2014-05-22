using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phone
{
    public class PhoneBook
    {
        public List<PhoneData> phoneList;
        
        public PhoneBook()
        {
            phoneList = new List<PhoneData>();
        }

        public void Add(string name, string number, string group)
        {
            PhoneData p = new PhoneData(name, number, group);
            phoneList.Add(p);
        }

        public PhoneData FindPeople(string name)
        {
            foreach (PhoneData a in phoneList)
            {
                if (a.mName == name)
                {
                    return a;
                }
            }
            return null;
        }

        public string GetAllData()
        {
            string result = "";
            foreach (PhoneData a in phoneList)
            {
                result += string.Format("{0}|{1}|{2}\r\n", a.mGroup, a.mName, a.mNumber);
            }
            return result;
        }
    }

    public class PhoneData
    {
        public string mName;
        public string mNumber;
        public string mGroup;
        
        public PhoneData()
        {
            mName = "";
            mNumber = "";
            mGroup = "";
        }

        public PhoneData(string name, string number, string group)
        {
            mName = name;
            mNumber = number;
            mGroup = group;
        }
        
    }
}
