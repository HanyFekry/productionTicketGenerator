using DBLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class SearchHelper<T> where T:class
    {
        public List<T> GetSearchResult(string param, string seperator = SD.SearchItemSeparator, string includeProperties = null) //where T : class
        {
            List<T> result = GetSearchResult(GetSearchFieldlst(param, seperator), includeProperties);
            return result;
        }
        public List<T> GetSearchResult(List<SearchField> searchFields, string includeProperties = null) //where T : class
        {
            using (var context = new ManufacturingContext())
            {
                DbSet<T> dbset = context.Set<T>();
                try
                {
                    IQueryable<T> query = dbset;
                    if (null != searchFields && searchFields.Count > 0)
                        query = dbset.Where(FilterLinq<T>.GetWherePredicate(searchFields.ToArray()));

                    if (!string.IsNullOrWhiteSpace(includeProperties))
                    {
                        string[] arrProprties = includeProperties.Split(",".ToCharArray(), (StringSplitOptions.RemoveEmptyEntries));
                        foreach (string property in arrProprties)
                            query = query.Include(property);
                    }
                    return query.ToList();
                }
                catch (Exception ex) { context.Dispose(); return null; }
            }

        }

        public List<SearchField> GetSearchFieldlst(string param, string seperator = SD.SearchItemSeparator)
        {
            List<SearchField> lstFields = new List<SearchField>();
            //List<SearchField> lstFields = new List<SearchField>();
            if (!string.IsNullOrEmpty(param))
            {
                if (param.Contains(seperator))
                {
                    param.Split(seperator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(i =>
                    {
                        GetSearchItem(ref lstFields, i);
                    });
                }
                else
                {
                    GetSearchItem(ref lstFields, param);
                }
            }
            return lstFields;
        }
        void GetSearchItem(ref List<SearchField> lstFields, string Item)
        {
            if (Item.Contains("=="))
                lstFields.Add(new SearchField(Item.Split("==".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].Trim(), Item.Split("==".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1].Trim(),Operator: "=="));
            if (Item.Contains("!="))
                lstFields.Add(new SearchField(Item.Split("!=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].Trim(), Item.Split("!=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1].Trim(),Operator: "!="));
            if (Item.Contains(QueryOperators.StartsWith))
                lstFields.Add(new SearchField(Item.Split(QueryOperators.StartsWith.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].Trim(), Item.Split(QueryOperators.StartsWith.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1].Trim(), Operator: QueryOperators.StartsWith));
        }
        public List<T> GetSearchResult(Dictionary<SearchField, string> searchFields, string includeProperties = null) //where T : class
        {
            using (var context = new ManufacturingContext())
            {
                DbSet<T> dbset = context.Set<T>();
                try
                {
                    IQueryable<T> query = dbset;
                    if (null != searchFields && searchFields.Count > 0)
                        query = dbset.Where(FilterLinq<T>.GetWherePredicate(searchFields));

                    if (!string.IsNullOrWhiteSpace(includeProperties))
                    {
                        string[] arrProprties = includeProperties.Split(",".ToCharArray(), (StringSplitOptions.RemoveEmptyEntries));
                        foreach (string property in arrProprties)
                            query = query.Include(property);
                    }
                    return query.ToList();
                }
                catch (Exception) { context.Dispose(); return null; }
            }

        }
        public Dictionary<SearchField, string> GetSearchFields(string param, string seperator = "$$$")
        {
            Dictionary<SearchField, string> dicFields = new Dictionary<SearchField, string>();
            //List<SearchField> lstFields = new List<SearchField>();
            if (!string.IsNullOrEmpty(param))
            {
                if (param.Contains(seperator))
                {
                    param.Split(seperator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(i =>
                    {
                        GetSearchItem(ref dicFields, i);
                    });
                }
                else
                {
                    GetSearchItem(ref dicFields, param);
                }
            }
            return dicFields;
        }
        void GetSearchItem(ref Dictionary<SearchField, string> dicFields, string Item)
        {
            if (Item.Contains("=="))
                dicFields.Add(new SearchField(Item.Split("==".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].Trim(), Item.Split("==".ToCharArray(),StringSplitOptions.RemoveEmptyEntries)[1].Trim()), "==");
            if (Item.Contains("!="))
                dicFields.Add(new SearchField(Item.Split("!=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].Trim(), Item.Split("!=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1].Trim()), "!=");
        }
    }
}
