namespace ComposingMethods
{

    /* Inline Temp
     * 
     * Context: You have a temp that is assigned to once
     * with a simple expression, and the temp is getting in
     * the way of other refactorings
     * 
     * Refactoring: Replace all references with the expression.
     * 
     * 
     * 
     */
    public class InlineTemp
    {
        public bool IsPricy(Order order)
        {
            double basePrice = order.BasePrice;
            return basePrice > 1000;
        }

        ////Refactored version
        //  public void IsPricy(Order order)
        //  {
        //    return order.BasePrice > 1000;
        //  }

         
    }

    public class Order
    {
        public int BasePrice { get; set; }
    }
}