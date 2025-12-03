using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model
{
    /// <summary>
    /// Vehicle Management Model
    /// </summary>
    public class OVHL
    {
        public int ID {  get; set; }
        public string VehicleRegistrationNo { get; set; } = string.Empty;
        public string VehicleModel {  get; set; }=string.Empty; 
        public VehicleRegistrationType RegistrationType {  get; set; } 
        public string SerialNumber {  get; set; } = string.Empty;
        public string OwnerID {  get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public string YearOfManufacture {  get; set; } = string.Empty;
        public string PlateNumber {  get; set; } = string.Empty;
        public DateTime? DrivingLicenseExpiry {  get; set; } = DateTime.Now;
        public string ChasisNumber {  get; set; } = string.Empty;
        public string VehicleWeight {  get; set; } = string.Empty;
        public string InsuranceCompany {  get; set; } = string.Empty;
        public string InsurancePolicyNumber {  get; set; } = string.Empty;
        public DateTime? InsuranceStartDate {  get; set; } = DateTime.Now;
        public DateTime? InsuranceExpiry { get; set; } = DateTime.Now;
        //public string ExpirationNotice {  get; set; } = string.Empty;
        public Status InsuranceStatus { get; set; }
        public DateTime? VehicleInspectionExpiryDate {  get; set; } = DateTime.Now;
        public DateTime? VehicleInspectionDueIn { get; set; } = DateTime.Now;
        public DateTime? VehicleRegistrationExpiry {  get; set; } = DateTime.Now;
        public DateTime? VehicleRegistrationExpiryIn { get; set; } = DateTime.Now;
        public DateTime? OperationCardExpiryDate {  get; set; } = DateTime.Now;
        public DateTime? OperationCardExpiryIn {  get; set; } = DateTime.Now;
        public DateTime? GPSTrackingExpiryDate {  get; set; } = DateTime.Now;
        public DateTime? GPSTrackingExpiryIn {  get; set; } = DateTime.Now;
        public string DriverName {  get; set; } = string.Empty;
        public string PhoneNumber {  get; set; } = string.Empty;
        public string IqamaNumber {  get; set; } = string.Empty;
        public Status PermissionStatus {  get; set; } 
        public DateTime? PermissionEndDate {  get; set; } = DateTime.Now;
        public DateTime? PermissionExpiresIn {  get; set; } = DateTime.Now;
        public int CompanyID {  get; set; }

        public OCMP Company {  get; set; }=new OCMP();
    }
}
