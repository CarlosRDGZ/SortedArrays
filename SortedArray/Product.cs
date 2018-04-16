using System;
namespace SortedArray
{
    public class Product
    {
        private static Random _random = new Random(DateTime.Now.Millisecond);

		#region Properties
		public string Code { get; }
        public string Name { get; set; }
        public string Description { get; set; }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { if (value >= 0) _quantity = value; }
        }

        private int _cost;
        public int Cost
        {
            get { return _cost; }
            set { if (value >= 0) _cost = value; }
        }
        #endregion
        
		public Product(string name, string description, int quantity, int cost)
        {
            DateTime now = DateTime.Now;
            Code = name.Substring(0, 3)
                + now.Millisecond
                + (now.Second > 9 ? now.Second.ToString() : _random.Next(10).ToString() + now.Second)
                + (now.Minute > 9 ? now.Minute.ToString() : _random.Next(10).ToString() + now.Minute);
            Name = name;
            Description = description;
            Quantity = quantity;
            Cost = cost;
        }

        public override string ToString() =>
            "{ " + string.Join(", ", PropsToStrings()) + " }";

        public string ToJSON() =>
            "{" + Environment.NewLine + "   " + 
                string.Join("," + Environment.NewLine + "   ", PropsToStrings()) +
                Environment.NewLine + "}";

        private string[] PropsToStrings()
        {
            System.Reflection.PropertyInfo[] props =
                this.GetType().GetProperties();
            string[] strs = new string[props.Length];
            for (int i = 0; i < props.Length; i++)
                strs[i] = props[i].Name + ": " + props[i].GetValue(this, null);
            return strs;
        }
    }
}