namespace ComposingMethods
{

    /* Inline Method
     * 
     * Context: A method's body is just as clear as its name
     * 
     * Refactoring: Put the code from the offending method into its caller and remove the method.
     * 
     * 
     * 
     */

    public class InlineMethod
    {
        public int NumberOfLateDeliveries { get; set; }

        public int GetRating()
        {
            return (MoreThanFiveLateDeliveries()) ? 2 : 1;
        }

        private bool MoreThanFiveLateDeliveries()
        {
            return NumberOfLateDeliveries > 5;
        }

        //// Refactored method
        //public int GetRating()
        //{
        //    return (NumberOfLateDeliveries > 5) ? 2 : 1;
        //}


    }
}