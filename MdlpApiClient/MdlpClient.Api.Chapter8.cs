﻿namespace MdlpApiClient
{
    using DataContracts;
    using RestSharp;

    /// <remarks>
    /// Strongly typed REST API methods. Chapter 8: MDLP information.
    /// </remarks>
    partial class MdlpClient
    {
        /// <summary>
        /// 8.1.2. Метод для поиска информации о местах осуществления деятельности по фильтру
        /// </summary>
        /// <param name="filter">Фильтр для поиска мест осуществления деятельности</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых мест</param>
        /// <param name="count">Количество записей в списке возвращаемых мест</param>
        /// <returns>Список мест осуществления деятельности</returns>
        public GetBranchesResponse GetBranches(BranchFilter filter, int startFrom, int count)
        {
            return Post<GetBranchesResponse>("reestr/branches/filter", new
            {
                filter = filter ?? new BranchFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.1.3. Получение информации о конкретном месте осуществления деятельности
        /// </summary>
        public GetBranchResponse GetBranch(string branchId)
        {
            return Get<GetBranchResponse>("reestr/branches/{branch_id}", new[]
            {
                new Parameter("branch_id", branchId, ParameterType.UrlSegment)
            });
        }

        /// <summary>
        /// 8.1.4. Метод для регистрация места осуществления деятельности
        /// </summary>
        public string RegisterBranch(Address address)
        {
            var branch = Post<GetBranchResponse>("reestr/branches/register", new
            {
                branch_address = address
            });

            return branch.BranchID;
        }

        /// <summary>
        /// 8.2.2. Метод для поиска информации о местах ответственного хранения по фильтру
        /// </summary>
        /// <param name="filter">Фильтр для поиска мест осуществления деятельности</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых мест</param>
        /// <param name="count">Количество записей в списке возвращаемых мест</param>
        /// <returns>Список мест ответственного хранения</returns>
        public GetWarehousesResponse GetWarehouses(WarehouseFilter filter, int startFrom, int count)
        {
            return Post<GetWarehousesResponse>("reestr/warehouses/filter", new
            {
                filter = filter ?? new WarehouseFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.2.3. Получение информации о конкретном месте ответственного хранения
        /// </summary>
        public GetWarehouseResponse GetWarehouses(string warehouseId)
        {
            return Get<GetWarehouseResponse>("reestr/warehouses/{warehouse_id}", new[]
            {
                new Parameter("warehouse_id", warehouseId, ParameterType.UrlSegment)
            });
        }
    }
}