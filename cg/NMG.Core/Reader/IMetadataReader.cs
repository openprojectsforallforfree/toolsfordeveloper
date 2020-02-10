using System.Collections.Generic;
using NMG.Core.Domain;

namespace NMG.Core.Reader
{
    public interface IMetadataReader
    {
        List<Column> GetTableDetails(Table table, string owner);
        List<Table> GetTables(string owner);
        List<string> GetOwners();
        List<string> GetSequences(string owner);
        PrimaryKey DeterminePrimaryKeys(Table table);
        List<ForeignKey> DetermineForeignKeyReferences(Table table);
        //List<string> GetSequences(List<Table> table);
        //List<string> GetForeignKeyTables(string columnName);
    }
}