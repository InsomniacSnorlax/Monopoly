namespace Monopoly.Main
{
    public sealed class Bank
    {
        public int Houses = 32;
        public int Hotels = 12;

        public static Bank Instance
        {
            get
            {
                if (m_Instance == null) m_Instance = new Bank();

                return m_Instance;
            }

        }

        private static Bank m_Instance;
    }
}
