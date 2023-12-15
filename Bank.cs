namespace Monopoly
{
    public sealed class Bank
    {
        public int Houses;
        public int Hotels;
        public int Money;

        public static Bank Instance
        {
            get
            {
                if(m_Instance == null) m_Instance= new Bank();

                return m_Instance;
            }

        }

        private static Bank m_Instance;
    }
}
