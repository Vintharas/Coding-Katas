namespace ComposingMethods
{
    /* Replace Temp With Query
     * 
     * Context: You are usign a temporary variable to hold the result
     * of an expression
     * 
     * Refactoring: Extract the expression into a method. Replace 
     * all references to the temp with the new method. The new method
     * can then be used in other methods
     * 
     */
    public class ReplaceTempWithQuery
    {
        public int Quantity { get; set; }
        public double ItemPrice { get; set; }

        public double Price {
            get { 
                double basePrice = Quantity*ItemPrice;
                double discountFactor = .0;
                if (basePrice > 1000)
                    discountFactor = 0.95;
                else
                    discountFactor = 0.98;
                return basePrice*discountFactor;
            }
        }
         
    }

    public class ReplaceTempWithQueryRefactored
    {
        public int Quantity { get; set; }
        public double ItemPrice { get; set; }

        public double Price
        {
            get { return BasePrice*DiscountFactor; }
        }

        public double BasePrice { get { return Quantity*ItemPrice; }}
        public double DiscountFactor { get { return BasePrice > 1000 ? 0.95 : 0.98; } }

    }
}