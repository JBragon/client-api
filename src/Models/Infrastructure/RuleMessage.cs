namespace Models.Infrastructure
{
    public static class RuleMessage
    {
        public static string Informed(string field)
        {
            return $"{field} {Resources.Common.Informed}";
        }

        public static string LessOrEqual(string field, int number)
        {
            return $"{field} {Resources.Common.LessOrEqual} {number} {Resources.Common.Characters}";
        }

        public static string Between(string field, decimal from, decimal to)
        {
            return $"{field} {Resources.Common.Between} {from} {Resources.Common.And} {to}";
        }

        public static string MaxLength(string field, int length)
        {
            return string.Format(Resources.Common.MaxLength, field, length);
        }
    }
}
