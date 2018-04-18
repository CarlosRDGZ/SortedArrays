using System;
namespace SortedArray
{
    public class Catalog
    {
        private readonly Product[] _catalog;
        private readonly int Length;

        private int _productsCount = 0;

        public Catalog(int numProducts)
        {
            _catalog = new Product[numProducts];
            Length = numProducts;
        }

        public void Add(Product product)
        {
			int index = Math.Min(_productsCount,Length - 1);
            int limit = index;
            for(int i = 0; i < limit; i++)
				if (string.Compare(product.Code, _catalog[i].Code, StringComparison.Ordinal) < 0)
                {
                    index = i;
                    break;
                }

            for (int i = limit; i > index; i--)
                _catalog[i] = _catalog[i - 1];
            _catalog[index] = product;

            if (_productsCount < _catalog.Length)
                _productsCount++;
        }

        public Product Search(string code)
        {
			Product Seek(int start, int end)
            {
				if (start <= end)
				{
					int half = (start + end) / 2;
					if (string.Compare(code, _catalog[half].Code, StringComparison.Ordinal) > 0)
						return Seek(half + 1, end);
					if (string.Compare(code, _catalog[half].Code, StringComparison.Ordinal) < 0)
						return Seek(start, half - 1);
					if (string.Compare(code, _catalog[half].Code, StringComparison.Ordinal) == 0)
						return _catalog[half];
				}
				return null;
			}
			return Seek(0, Math.Min(_productsCount, Length));
        }

        public void Delete(string code)
        {
			int Seek(int start, int end)
			{
				if (start <= end)
				{
					int half = (start + end) / 2;
					if (string.Compare(code, _catalog[half].Code, StringComparison.Ordinal) > 0)
						return Seek(half + 1, end);
					if (string.Compare(code, _catalog[half].Code, StringComparison.Ordinal) < 0)
						return Seek(start, half - 1);
					if (string.Compare(code, _catalog[half].Code, StringComparison.Ordinal) == 0)
						return half;
				}
				return -1;
			}

			int index = Seek(0, Math.Min(_productsCount, Length));
			if (index == -1)
				throw new Exception("Not Found");
			int last = Math.Min(_productsCount, Length - 1);
			for (int i = index, j = last; i < j; i++)
				_catalog[i] = _catalog[i + 1];
			_catalog[last] = null;

			_productsCount--;
        }

        public string List()
        {
            string str = "[" + Environment.NewLine;
			for (int i = 0; i < _productsCount; i++)
                if (_catalog[i] != null)
                    str += (
                        "   "
                        + _catalog[i]
						+ (i < _productsCount - 1 ? ", " : "")
                        + Environment.NewLine
                    );
            return str + "]";
        }   
    }
}
