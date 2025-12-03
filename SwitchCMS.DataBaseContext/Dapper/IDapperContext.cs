using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.DataBaseContext.Dapper
{
    public interface IDapperContext
    {
        bool AllowEmptyUpdate { get; set; }
        int DefaultTimeout { get; set; }
        bool IsInTransaction { get; }

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        Task<IEnumerable<T>> QueryAsync<T>(string query) where T : class, new();
        Task<IEnumerable<T>> QueryAsync<T>(string query, DynamicParameters param) where T : class, new();
        Task<T> QueryFirstAsync<T>(string query, DynamicParameters param) where T : class, new();
        Task<T> QuerySingleAsync<T>(string query, DynamicParameters param);
        Task<int> ExecuteAsync(string query, DynamicParameters param);
        Task<T> ExecuteScalarAsync<T>(string query, DynamicParameters param);
        Task<T> QueryFirstOrDefaultAsync<T>(string query, DynamicParameters param);
        Task<int> ExecuteBulkAsync<T>(string query, List<DynamicParameters> param);
        Task<IEnumerable<T>> QueryAsync<T1, T2, T>(string query, Func<T1, T2, T> func, string SplitOn, DynamicParameters param) where T : class, new();
        Task<IEnumerable<T>> QueryAsync<T1, T2, T3, T>(string query, Func<T1, T2, T3, T> func, string SplitOn, DynamicParameters param) where T : class, new();
        Task<IEnumerable<T>> QueryAsync<T1, T2, T3, T4, T>(string query, Func<T1, T2, T3, T4, T> func, string SplitOn, DynamicParameters param) where T : class, new();
        Task<IEnumerable<T>> QueryAsync<T1, T2, T3, T4, T5, T>(string query, Func<T1, T2, T3, T4, T5, T> func, string SplitOn, DynamicParameters param) where T : class, new();

        Task<T> QueryFirstOrDefaultAsync<T1, T2, T>(string query, Func<T1, T2, T> func, string SplitOn, DynamicParameters param) where T : class, new();
        Task<T> QueryFirstOrDefaultAsync<T1, T2, T3, T>(string query, Func<T1, T2, T3, T> func, string SplitOn, DynamicParameters param) where T : class, new();
        Task<T> QueryFirstOrDefaultAsync<T1, T2, T3, T4, T>(string query, Func<T1, T2, T3, T4, T> func, string SplitOn, DynamicParameters param) where T : class, new();

    }
}
