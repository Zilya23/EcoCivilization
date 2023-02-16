namespace EcoCivilizationAPI.Models
{
    public partial class EcoCivilizationContext
    {
        private static EcoCivilizationContext _context;

        public static EcoCivilizationContext GetContext()
        {
            if (_context == null)
                _context= new EcoCivilizationContext();

            return _context;
        }
    }
}
