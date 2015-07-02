using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataAccess.Utility
{
    public static class Extensions
    {

        public static IEnumerable<SelectListItem> GetEnumSelectList<T>()
        {
            List<SelectListItem> listItem =new List<SelectListItem>();
            Array sizeArray = Enum.GetValues(typeof(T));
            foreach (T val in sizeArray)
            {
                listItem.Add(new SelectListItem { Text=val.ToString(),Value=Convert.ToInt32(val).ToString() });
            }
            return listItem;
            //return (Enum.GetValues(typeof(T)).Cast<T>().Select(
              //  enu => new SelectListItem() { Text = enu.ToString(), Value = enu.ToString() })).ToList();
        }

        public static string GetDisplayName(Enum value)
        {
            var type = value.GetType();
            if (!type.IsEnum) throw new ArgumentException(String.Format("Type '{0}' is not Enum", type));

            var members = type.GetMember(value.ToString());
            if (members.Length == 0) throw new ArgumentException(String.Format("Member '{0}' not found in type '{1}'", value, type.Name));

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attributes.Length == 0) throw new ArgumentException(String.Format("'{0}.{1}' doesn't have DisplayAttribute", type.Name, value));

            var attribute = (DisplayAttribute)attributes[0];
            return attribute.GetName();
        }
    }
}