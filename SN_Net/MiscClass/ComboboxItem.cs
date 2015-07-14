using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SN_Net.MiscClass
{
    public class ComboboxItem
    {
        public string name;
        public int int_value;
        public string string_value;

        public ComboboxItem(string name, int int_value, string string_value)
        {
            this.name = name;
            this.int_value = int_value;
            this.string_value = string_value;
        }

        public override string ToString()
        {
            return this.name;
        }

        /// <summary>
        /// Get the item Name (string human readable as combobox shown)
        /// </summary>
        /// <param name="cb">The Combobox to find a item Name</param>
        /// <param name="int_value">The int_value to use as a key to find</param>
        /// <returns>Name of the item (as combobox shown)</returns>
        public static string GetItemText(ComboBox cb, int int_value)
        {
            string item_text = string.Empty;
            foreach (var item in cb.Items)
            {
                ComboboxItem ci = item as ComboboxItem;
                if (ci.int_value == int_value)
                {
                    item_text = ci.name;
                }
            }

            return item_text;
        }

        /// <summary>
        /// Get the item Name from ComboboxItem(string human readable as combobox shown) 
        /// </summary>
        /// <param name="cb">The Combobox to find a item Name</param>
        /// <param name="string_value">The string_value to use as a key to find</param>
        /// <returns>(string)Name of the item (as combobox shown)</returns>
        public static string GetItemText(ComboBox cb, string string_value)
        {
            string item_text = string.Empty;
            foreach (var item in cb.Items)
            {
                ComboboxItem ci = item as ComboboxItem;
                if (ci.string_value == string_value)
                {
                    item_text = ci.name;
                }
            }

            return item_text;
        }

        /// <summary>
        /// Get the index of ComboboxItem in Combobox by pasing int_value
        /// </summary>
        /// <param name="cb">The Combobox to find index</param>
        /// <param name="int_value">The int_value to use as a key to find an index</param>
        /// <returns>(int) Index of the ComboboxItem</returns>
        public static int GetItemIndex(ComboBox cb, int int_value){
            int item_index = -1;

            int item_count = 0;
            foreach(var item in cb.Items){
                ComboboxItem ci = item as ComboboxItem;
                if (ci.int_value == int_value)
                {
                    item_index = item_count;
                }
                item_count++;
            }

            return item_index;
        }

        /// <summary>
        /// Get the index of ComboboxItem in Combobox by pasing int_value
        /// </summary>
        /// <param name="cb">The Combobox to find index</param>
        /// <param name="string_value">The string_value to use as a key to find an index</param>
        /// <returns>(int) Index of the ComboboxItem</returns>
        public static int GetItemIndex(ComboBox cb, string string_value)
        {
            int item_index = -1;

            int item_count = 0;
            foreach (var item in cb.Items)
            {
                ComboboxItem ci = item as ComboboxItem;
                if (ci.string_value == string_value)
                {
                    item_index = item_count;
                }
                item_count++;
            }

            return item_index;
        }
    }
}
