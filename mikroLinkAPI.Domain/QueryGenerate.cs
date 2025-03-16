namespace mikroLinkAPI.Domain
{
    public static class QueryGenerate
    {
        public static string ComponentAddValidationQuery(string filepath)
        {
            return ConvertSql("ComponentAddValidationQuery", filepath);
        }
        public static string ComponentAddQuery(string filepath)
        {
            return ConvertSql("ComponentAddQuery", filepath);
        }
        public static string MetarialAddValidationQuery(string filepath, int Company, int User)
        {
            var keys = new Dictionary<string, string>();
            keys.Add("{User}", User.ToString());
            keys.Add("{Company}", Company.ToString());
            return ConvertSql("MetarialAddValidationQuery", filepath, keys);
        }
        public static string MetarialAddQuery(string filepath, int Company, int User)
        {
            var keys = new Dictionary<string, string>();
            keys.Add("{User}", User.ToString());
            keys.Add("{Company}", Company.ToString());
            return ConvertSql("MetarialAddQuery", filepath, keys);
        }
        public static string SiteMetarialAddValidationQuery(string filepath, int User)
        {
            var keys = new Dictionary<string, string>();
            keys.Add("{User}", User.ToString());
            return ConvertSql("SiteMetarialAddValidationQuery", filepath, keys);
        }
        public static string SiteMetarialAddQuery(string filepath, int User)
        {
            var keys = new Dictionary<string, string>();
            keys.Add("{User}", User.ToString());
            return ConvertSql("SiteMetarialAddQuery", filepath, keys);
        }
        private static string ConvertSql(string sqlpath, string filepath, Dictionary<string, string> keyvalue = null)
        {
            var sql = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "SqlQueries", sqlpath+".sql"));
            sql = sql.Replace("_[@tempName]", RandomString(5));
            sql = sql.Replace("{filepath}", filepath);
            if (keyvalue != null)
            {
                foreach (var item in keyvalue)
                {
                    sql = sql.Replace(item.Key, item.Value);
                }
            }
            return sql;
        }

        private static string RandomString(int length)
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
