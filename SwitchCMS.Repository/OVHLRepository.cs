using Dapper;
using SwitchCMS.DataBaseContext.Dapper;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository
{
    public class OVHLRepository : IOVHLRepository
    {

        private readonly IDapperContext DbContext;
        public OVHLRepository(IDapperContext _DbContext)
        {
            DbContext = _DbContext;
        }
        public async Task<bool> CreateVehicle(OVHL Model)
        {
            try
            {


                string SqlQuery = @"INSERT INTO [dbo].[OVHL]
                               ([VehicleRegistrationNo]
                               ,[VehicleModel]
                               ,[RegistrationType]
                               ,[SerialNumber]
                               ,[OwnerID]
                               ,[OwnerName]
                               ,[YearOfManufacture]
                               ,[PlateNumber]
                               ,[DrivingLicenseExpiry]
                               ,[ChasisNumber]
                               ,[VehicleWeight]
                               ,[InsuranceCompany]
                               ,[InsurancePolicyNumber]
                               ,[InsuranceStartDate]
                               ,[InsuranceExpiry]
                               ,[InsuranceStatus]
                               ,[VehicleInspectionExpiryDate]
                               ,[VehicleInspectionDueIn]
                               ,[VehicleRegistrationExpiry]
                               ,[VehicleRegistrationExpiryIn]
                               ,[OperationCardExpiryDate]
                               ,[OperationCardExpiryIn]
                               ,[GPSTrackingExpiryDate]
                               ,[GPSTrackingExpiryIn]
                               ,[DriverName]
                               ,[PhoneNumber]
                               ,[IqamaNumber]
                               ,[PermissionStatus]
                               ,[PermissionEndDate]
                               ,[PermissionExpiresIn]
                               ,[CompanyID])
                                VALUES
                               (@VehicleRegistrationNo 
                               ,@VehicleModel 
                               ,@RegistrationType 
                               ,@SerialNumber 
                               ,@OwnerID 
                               ,@OwnerName 
                               ,@YearOfManufacture 
                               ,@PlateNumber 
                               ,@DrivingLicenseExpiry 
                               ,@ChasisNumber 
                               ,@VehicleWeight 
                               ,@InsuranceCompany 
                               ,@InsurancePolicyNumber 
                               ,@InsuranceStartDate 
                               ,@InsuranceExpiry 
                               ,@InsuranceStatus 
                               ,@VehicleInspectionExpiryDate 
                               ,@VehicleInspectionDueIn 
                               ,@VehicleRegistrationExpiry 
                               ,@VehicleRegistrationExpiryIn 
                               ,@OperationCardExpiryDate 
                               ,@OperationCardExpiryIn 
                               ,@GPSTrackingExpiryDate 
                               ,@GPSTrackingExpiryIn 
                               ,@DriverName 
                               ,@PhoneNumber 
                               ,@IqamaNumber 
                               ,@PermissionStatus 
                               ,@PermissionEndDate 
                               ,@PermissionExpiresIn 
                               ,@CompanyID )";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("VehicleRegistrationNo", Model.VehicleRegistrationNo);
                parameters.Add("VehicleModel", Model.VehicleModel);
                parameters.Add("RegistrationType", Model.RegistrationType.ToString());
                parameters.Add("SerialNumber", Model.SerialNumber);
                parameters.Add("OwnerID", Model.OwnerID);
                parameters.Add("OwnerName", Model.OwnerName);
                parameters.Add("YearOfManufacture", Model.YearOfManufacture);
                parameters.Add("PlateNumber", Model.PlateNumber);
                parameters.Add("DrivingLicenseExpiry", Model.DrivingLicenseExpiry);
                parameters.Add("ChasisNumber", Model.ChasisNumber);
                parameters.Add("VehicleWeight", Model.VehicleWeight);
                parameters.Add("InsuranceCompany", Model.InsuranceCompany);
                parameters.Add("InsurancePolicyNumber", Model.InsurancePolicyNumber);
                parameters.Add("InsuranceStartDate", Model.InsuranceStartDate);
                parameters.Add("InsuranceExpiry", Model.InsuranceExpiry);
                parameters.Add("InsuranceStatus", Model.InsuranceStatus.ToString());
                parameters.Add("VehicleInspectionExpiryDate", Model.VehicleInspectionExpiryDate);
                parameters.Add("VehicleInspectionDueIn", Model.VehicleInspectionDueIn);
                parameters.Add("VehicleRegistrationExpiry", Model.VehicleRegistrationExpiry);
                parameters.Add("VehicleRegistrationExpiryIn", Model.VehicleRegistrationExpiryIn);
                parameters.Add("OperationCardExpiryDate", Model.OperationCardExpiryDate);
                parameters.Add("OperationCardExpiryIn", Model.OperationCardExpiryIn);
                parameters.Add("GPSTrackingExpiryDate", Model.GPSTrackingExpiryDate);
                parameters.Add("GPSTrackingExpiryIn", Model.GPSTrackingExpiryIn);
                parameters.Add("DriverName", Model.DriverName);
                parameters.Add("PhoneNumber", Model.PhoneNumber);
                parameters.Add("IqamaNumber", Model.IqamaNumber);
                parameters.Add("PermissionStatus", Model.PermissionStatus.ToString());
                parameters.Add("PermissionEndDate", Model.PermissionEndDate);
                parameters.Add("PermissionExpiresIn", Model.PermissionExpiresIn);
                parameters.Add("CompanyID", Model.CompanyID);

                int Success = await DbContext.ExecuteAsync(SqlQuery, parameters);

                return Success > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }

             
        }


        public async Task<bool> BulkCreateVehicle(List<OVHL> List)
        {
            string SqlQuery = @"INSERT INTO [dbo].[OVHL]
                               ([VehicleRegistrationNo]
                               ,[VehicleModel]
                               ,[RegistrationType]
                               ,[SerialNumber]
                               ,[OwnerID]
                               ,[OwnerName]
                               ,[YearOfManufacture]
                               ,[PlateNumber]
                               ,[DrivingLicenseExpiry]
                               ,[ChasisNumber]
                               ,[VehicleWeight]
                               ,[InsuranceCompany]
                               ,[InsurancePolicyNumber]
                               ,[InsuranceStartDate]
                               ,[InsuranceExpiry]
                               ,[InsuranceStatus]
                               ,[VehicleInspectionExpiryDate]
                               ,[VehicleInspectionDueIn]
                               ,[VehicleRegistrationExpiry]
                               ,[VehicleRegistrationExpiryIn]
                               ,[OperationCardExpiryDate]
                               ,[OperationCardExpiryIn]
                               ,[GPSTrackingExpiryDate]
                               ,[GPSTrackingExpiryIn]
                               ,[DriverName]
                               ,[PhoneNumber]
                               ,[IqamaNumber]
                               ,[PermissionStatus]
                               ,[PermissionEndDate]
                               ,[PermissionExpiresIn]
                               ,[CompanyID])
                                VALUES
                               (@VehicleRegistrationNo 
                               ,@VehicleModel 
                               ,@RegistrationType 
                               ,@SerialNumber 
                               ,@OwnerID 
                               ,@OwnerName 
                               ,@YearOfManufacture 
                               ,@PlateNumber 
                               ,@DrivingLicenseExpiry 
                               ,@ChasisNumber 
                               ,@VehicleWeight 
                               ,@InsuranceCompany 
                               ,@InsurancePolicyNumber 
                               ,@InsuranceStartDate 
                               ,@InsuranceExpiry 
                               ,@InsuranceStatus 
                               ,@VehicleInspectionExpiryDate 
                               ,@VehicleInspectionDueIn 
                               ,@VehicleRegistrationExpiry 
                               ,@VehicleRegistrationExpiryIn 
                               ,@OperationCardExpiryDate 
                               ,@OpedationCardExpiryIn 
                               ,@GPSTrackingExpiryDate 
                               ,@GPSTrackingExpiryIn 
                               ,@DriverName 
                               ,@PhoneNumber 
                               ,@IqamaNumber 
                               ,@PermissionStatus 
                               ,@PermissionEndDate 
                               ,@PermissionExpiresIn 
                               ,@CompanyID )";

            List<DynamicParameters> ParameterList= new List<DynamicParameters>();
            foreach(var Model in List)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("VehicleRegistrationNo", Model.VehicleRegistrationNo);
                parameters.Add("VehicleModel", Model.VehicleModel);
                parameters.Add("RegistrationType", Model.RegistrationType.ToString());
                parameters.Add("SerialNumber", Model.SerialNumber);
                parameters.Add("OwnerID", Model.OwnerID);
                parameters.Add("OwnerName", Model.OwnerName);
                parameters.Add("YearOfManufacture", Model.YearOfManufacture);
                parameters.Add("PlateNumber", Model.PlateNumber);
                parameters.Add("DrivingLicenseExpiry", Model.DrivingLicenseExpiry);
                parameters.Add("ChasisNumber", Model.ChasisNumber);
                parameters.Add("VehicleWeight", Model.VehicleWeight);
                parameters.Add("InsuranceCompany", Model.InsuranceCompany);
                parameters.Add("InsurancePolicyNumber", Model.InsurancePolicyNumber);
                parameters.Add("InsuranceStartDate", Model.InsuranceStartDate);
                parameters.Add("InsuranceExpiry", Model.InsuranceExpiry);
                parameters.Add("InsuranceStatus", Model.InsuranceStatus.ToString());
                parameters.Add("VehicleInspectionExpiryDate", Model.VehicleInspectionExpiryDate);
                parameters.Add("VehicleInspectionDueIn", Model.VehicleInspectionDueIn);
                parameters.Add("VehicleRegistrationExpiry", Model.VehicleRegistrationExpiry);
                parameters.Add("VehicleRegistrationExpiryIn", Model.VehicleRegistrationExpiryIn);
                parameters.Add("OperationCardExpiryDate", Model.OperationCardExpiryDate);
                parameters.Add("OperationCardExpiryIn", Model.OperationCardExpiryIn);
                parameters.Add("GPSTrackingExpiryDate", Model.GPSTrackingExpiryDate);
                parameters.Add("GPSTrackingExpiryIn", Model.GPSTrackingExpiryIn);
                parameters.Add("DriverName", Model.DriverName);
                parameters.Add("PhoneNumber", Model.PhoneNumber);
                parameters.Add("IqamaNumber", Model.IqamaNumber);
                parameters.Add("PermissionStatus", Model.PermissionStatus.ToString());
                parameters.Add("PermissionEndDate", Model.PermissionEndDate);
                parameters.Add("PermissionExpiresIn", Model.PermissionExpiresIn);
                parameters.Add("CompanyID", Model.CompanyID);

                ParameterList.Add(parameters);
            }

            int Success = await DbContext.ExecuteBulkAsync<int>(SqlQuery, ParameterList);
            return Success > 0;
             
        }

       
        public async Task<bool> UpdateVehicle(OVHL Model)
        {
            string SqlQuery = @"UPDATE [dbo].[OVHL]
                               SET [VehicleRegistrationNo] = @VehicleRegistrationNo 
                              ,[VehicleModel] = @VehicleModel
                              ,[RegistrationType] = @RegistrationType
                              ,[SerialNumber] = @SerialNumber 
                              ,[OwnerID] = @OwnerID 
                              ,[OwnerName] = @OwnerName 
                              ,[YearOfManufacture] = @YearOfManufacture 
                              ,[PlateNumber] = @PlateNumber 
                              ,[DrivingLicenseExpiry] = @DrivingLicenseExpiry 
                              ,[ChasisNumber] = @ChasisNumber 
                              ,[VehicleWeight] = @VehicleWeight 
                              ,[InsuranceCompany] = @InsuranceCompany 
                              ,[InsurancePolicyNumber] = @InsurancePolicyNumber 
                              ,[InsuranceStartDate] = @InsuranceStartDate 
                              ,[InsuranceExpiry] = @InsuranceExpiry 
                              ,[InsuranceStatus] = @InsuranceStatus 
                              ,[VehicleInspectionExpiryDate] = @VehicleInspectionExpiryDate 
                              ,[VehicleInspectionDueIn] = @VehicleInspectionDueIn 
                              ,[VehicleRegistrationExpiry] = @VehicleRegistrationExpiry 
                              ,[VehicleRegistrationExpiryIn] = @VehicleRegistrationExpiryIn 
                              ,[OperationCardExpiryDate] = @OperationCardExpiryDate 
                              ,[OperationCardExpiryIn] = @OperationCardExpiryIn 
                              ,[GPSTrackingExpiryDate] = @GPSTrackingExpiryDate 
                              ,[GPSTrackingExpiryIn] = @GPSTrackingExpiryIn 
                              ,[DriverName] = @DriverName 
                              ,[PhoneNumber] = @PhoneNumber 
                              ,[IqamaNumber] = @IqamaNumber 
                              ,[PermissionStatus] = @PermissionStatus 
                              ,[PermissionEndDate] = @PermissionEndDate 
                              ,[PermissionExpiresIn] = @PermissionExpiresIn 
                              ,[CompanyID] = @CompanyID 
                              WHERE  ID=@ID";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("VehicleRegistrationNo", Model.VehicleRegistrationNo);
            parameters.Add("VehicleModel", Model.VehicleModel);
            parameters.Add("RegistrationType", Model.RegistrationType.ToString());
            parameters.Add("SerialNumber", Model.SerialNumber);
            parameters.Add("OwnerID", Model.OwnerID);
            parameters.Add("OwnerName", Model.OwnerName);
            parameters.Add("YearOfManufacture", Model.YearOfManufacture);
            parameters.Add("PlateNumber", Model.PlateNumber);
            parameters.Add("DrivingLicenseExpiry", Model.DrivingLicenseExpiry);
            parameters.Add("ChasisNumber", Model.ChasisNumber);
            parameters.Add("VehicleWeight", Model.VehicleWeight);
            parameters.Add("InsuranceCompany", Model.InsuranceCompany);
            parameters.Add("InsurancePolicyNumber", Model.InsurancePolicyNumber);
            parameters.Add("InsuranceStartDate", Model.InsuranceStartDate);
            parameters.Add("InsuranceExpiry", Model.InsuranceExpiry);
            parameters.Add("InsuranceStatus", Model.InsuranceStatus.ToString());
            parameters.Add("VehicleInspectionExpiryDate", Model.VehicleInspectionExpiryDate);
            parameters.Add("VehicleInspectionDueIn", Model.VehicleInspectionDueIn);
            parameters.Add("VehicleRegistrationExpiry", Model.VehicleRegistrationExpiry);
            parameters.Add("VehicleRegistrationExpiryIn", Model.VehicleRegistrationExpiryIn);
            parameters.Add("OperationCardExpiryDate", Model.OperationCardExpiryDate);
            parameters.Add("OperationCardExpiryIn", Model.OperationCardExpiryIn);
            parameters.Add("GPSTrackingExpiryDate", Model.GPSTrackingExpiryDate);
            parameters.Add("GPSTrackingExpiryIn", Model.GPSTrackingExpiryIn);
            parameters.Add("DriverName", Model.DriverName);
            parameters.Add("PhoneNumber", Model.PhoneNumber);
            parameters.Add("IqamaNumber", Model.IqamaNumber);
            parameters.Add("PermissionStatus", Model.PermissionStatus.ToString());
            parameters.Add("PermissionEndDate", Model.PermissionEndDate);
            parameters.Add("PermissionExpiresIn", Model.PermissionExpiresIn);
            parameters.Add("CompanyID", Model.CompanyID);
            parameters.Add("ID", Model.ID);


            int Success = await DbContext.ExecuteAsync(SqlQuery, parameters);

            return Success > 0;
             
        }

        public async Task<List<OVHL>> GetVehicleByPageIndex(VehiclePagination Pagination)
        {
            int offset = Pagination.RowCount * Pagination.PageIndex;
            string SqlQuery = @"select * from OVHL
                                where CompanyID=@CompanyID
                                and (VehicleRegistrationNo like @filterName or 
                                VehicleModel like @filterName or OwnerName like @filterName 
                                or OwnerID like @filterName or PlateNumber like @filterName)
                                ORDER BY OVHL.ID
                                OFFSET @offset ROWS FETCH NEXT @RowCount ROWS ONLY ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("filterName", "%"+ Pagination.FilterSearch+"%");
            parameters.Add("offset", offset);
            parameters.Add("RowCount", Pagination.RowCount);
            parameters.Add("CompanyID", Pagination.CompanyID);

            IEnumerable<OVHL> vehcileList = await DbContext.QueryAsync<OVHL>(SqlQuery, parameters);
            return vehcileList.ToList();
        }

        public async Task<int> GetVehicleCount(VehiclePagination Pagination)
        {
            string SqlQuery = @"select Count(ID) from OVHL
                                where CompanyID=@CompanyID
                                and (VehicleRegistrationNo like @filterName or 
                                VehicleModel like @filterName or OwnerName like @filterName 
                                or OwnerID like @filterName or PlateNumber like @filterName) ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("filterName", "%" + Pagination.FilterSearch + "%");
            parameters.Add("CompanyID", Pagination.CompanyID);
            int TotalCount = await DbContext.QuerySingleAsync<int>(SqlQuery, parameters);
            return TotalCount;

             
        }

        public async Task<OVHL> GetVehicleByID(int VehicleID)
        {
            string SqlQuery = "select * from OVHL where ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", VehicleID);
            var Data = await DbContext.QueryFirstOrDefaultAsync<OVHL>(SqlQuery, parameters);
            return Data;    
            
        }

        public async Task<bool> DeleteVehicle(int VehicleID)
        {
            string SqlQuery = "delete from OVHL where ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", VehicleID);
            int Success=await DbContext.ExecuteAsync(SqlQuery, parameters);
            return Success > 0;
             
        }
    }
}
