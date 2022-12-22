using Data4SalesChallenge.Attributes;
using System.Reflection;

namespace Data4SalesChallenge.Model
{
    public abstract class DataModelBase
    {
        protected string? _primaryKeyField;
        protected List<string> _props = new();

        public DataModelBase()
        {
            PropertyInfo? pkProp = GetType().GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).Length > 0)
                .FirstOrDefault();

            if (pkProp != null)
            {
                _primaryKeyField = pkProp.Name;
            }

            foreach (PropertyInfo prop in GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(DataFieldAttribute), false).Length > 0))
            {
                _props.Add(prop.Name);
            }
        }

        public virtual string TableName()
        {
            return GetType().GetCustomAttribute<TableAttribute>()?.Name ?? throw new Exception("Falta declarar el atributo Table");
        }

        //public virtual string CreateStatement
        //{
        //    get
        //    {
        //        string createQuery = "CREATE TABLE Films (PersonID int, LastName varchar(255), FirstName varchar(255), Address varchar(255), City varchar(255));";

        //        return string.Format("CREATE TABLE IF NOT EXISTS [{0}] ({1})",
        //            TableName,
        //            GetDelimitedSafeFieldList(", "));
        //    }
        //}

        public virtual string DropStatement()
        {
            return string.Format("DROP TABLE IF EXISTS {0};",
                TableName());

        }
        public virtual string SelectAllRecords()
        {
            return string.Format("SELECT * FROM {0}",
                TableName());
        }

        public virtual string SelectRecord(string pkName)
        {
            return string.Format("SELECT * FROM {0} WHERE {1} = @{1}",
                TableName(),
                pkName);
        }

        public virtual string InsertStatement()
        {
            return string.Format("INSERT INTO {0} ({1}) VALUES ({2}); SELECT @@IDENTITY;",
                TableName(),
                GetDelimitedSafeFieldList(", "),
                GetDelimitedSafeParamList(", "));
        }

        public virtual string UpdateStatement(string pkName)
        {
            return string.Format("UPDATE {0} SET {1} WHERE {2} = @{2}",
                TableName(),
                GetDelimitedSafeSetList(", "),
                pkName);
        }

        public virtual string DeleteStatement()
        {
            return string.Format("DELETE FROM {0} WHERE {1} = @{1}",
                TableName(),
                _primaryKeyField);
        }

        public virtual string SelectStatement()
        {
            return string.Format("SELECT [{0}], {1} FROM [{2}]",
                _primaryKeyField,
                GetDelimitedSafeFieldList(", "),
                TableName());
        }

        protected string GetDelimitedSafeParamList(string delimiter)
        {
            return string.Join(delimiter, _props.Select(k => string.Format("@{0}", k)));
        }

        protected string GetDelimitedSafeFieldList(string delimiter)
        {
            return string.Join(delimiter, _props.Select(k => string.Format("{0}", k)));
        }

        protected string GetDelimitedSafeSetList(string delimiter)
        {
            return string.Join(delimiter, _props.Select(k => string.Format("{0} = @{0}", k)));
        }
    }
}
