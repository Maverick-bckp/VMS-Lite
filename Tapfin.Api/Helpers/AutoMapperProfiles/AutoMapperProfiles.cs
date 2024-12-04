using AutoMapper;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Models.ViewModels;
using static Tapfin.Api.Models.DTO.ClientRevenueDto;

namespace Tapfin.Api.Helpers.AutoMapperProfiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            /*-------------- Region --------------*/
            #region
            CreateMap<Region, CreateRegionDtoRequest>();
            CreateMap<CreateRegionDtoRequest, Region>();

            CreateMap<GetRegionDtoResponse, Region>();
            CreateMap<Region, GetRegionDtoResponse>();

            CreateMap<Region, UpdateRegionDtoRequest>();
            CreateMap<UpdateRegionDtoRequest, Region>();
            #endregion


            /*-------------- Country--------------*/
            #region
            CreateMap<Country, CreateCountryDtoRequest>();
            CreateMap<CreateCountryDtoRequest, Country>();

            CreateMap<GetCountryDtoResponse, Country>();
            CreateMap<Country, GetCountryDtoResponse>();

            CreateMap<Country, UpdateCountryDtoRequest>();
            CreateMap<UpdateCountryDtoRequest, Country>();

            CreateMap<Region, CountryRegionDto>();
            CreateMap<CountryRegionDto, Region>();
            #endregion

            #region
            /*-------------- Client Details --------------*/
            CreateMap<ClientDetail, CreateClientDtoRequest>();
            CreateMap<CreateClientDtoRequest, ClientDetail>();

            CreateMap<GetClientDtoResponse, ClientDetail>();
            CreateMap<ClientDetail, GetClientDtoResponse>();

            CreateMap<ClientDetail, UpdateClientDtoRequest>();
            CreateMap<UpdateClientDtoRequest, ClientDetail>();

            CreateMap<Region, ClientRegionDto>();
            CreateMap<ClientRegionDto, Region>();

            CreateMap<ClientAddress, GetClientAddressDtoResponse>();
            CreateMap<GetClientAddressDtoResponse, ClientAddress>();

            CreateMap<ClientRevenue, GetClientDetailsRevenueDtoResponse>();
            CreateMap<GetClientDetailsRevenueDtoResponse, ClientRevenue>();

            CreateMap<Country, ClientCountryDto>();
            CreateMap<ClientCountryDto, Country>();

            CreateMap<ClientAddress, CreateClientAddressDto>();
            CreateMap<CreateClientAddressDto, ClientAddress>();

            CreateMap<ClientAddress, UpdateClientDetailsAddressDto>();
            CreateMap<UpdateClientDetailsAddressDto, ClientAddress>();

            CreateMap<User, GetClientUsersDtoResponse>();
            CreateMap<GetClientUsersDtoResponse, User>();
            #endregion

            /*-------------- Client revenue --------------*/
            #region
            CreateMap<ClientRevenue, GetClientRevenueDtoResponse>();
            CreateMap<GetClientRevenueDtoResponse, ClientRevenue>();

            CreateMap<CreateClientRevenueDtoRequest, ClientRevenue>();
            CreateMap<ClientRevenue, CreateClientRevenueDtoRequest>();

            CreateMap<ClientRevenue, UpdateClientRevenueDtoRequest>();
            CreateMap<UpdateClientRevenueDtoRequest, ClientRevenue>();

            CreateMap<ClientDetail, GetClientRevenueClientDetailsDtoResponse>();
            CreateMap<GetClientRevenueClientDetailsDtoResponse, ClientDetail>();
            #endregion

            #region
            /*-------------- Accounts --------------*/
            CreateMap<RegisterUserDtoRequest, User>().ForMember(m => m.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<User, RegisterUserDtoRequest>();

            CreateMap<User, UpdateUserDetailsDtoRequest>();
            CreateMap<UpdateUserDetailsDtoRequest, User>();

            CreateMap<User, UserProfileResponseDto>();
            CreateMap<UserProfileResponseDto, User>();

            CreateMap<Region, UserProfileRegionDto>();
            CreateMap<UserProfileRegionDto, Region>();

            CreateMap<Country, UserProfileCountryDto>();
            CreateMap<UserProfileCountryDto, Country>();

            CreateMap<ClientDetail, UserProfileClientDto>();
            CreateMap<UserProfileClientDto, ClientDetail>();

            CreateMap<VendorDetail, UserProfileVendorDto>();
            CreateMap<UserProfileVendorDto, VendorDetail>();

            CreateMap<User, UserBasicdetailsDto>();
            CreateMap<UserBasicdetailsDto, User>();
            #endregion

            #region
            /*-------------- Vendor Details --------------*/
            CreateMap<VendorDetail, CreateVendorDtoRequest>();
            CreateMap<CreateVendorDtoRequest, VendorDetail>();

            CreateMap<GetVendorDetailsDtoResponse, VendorDetail>();
            CreateMap<VendorDetail, GetVendorDetailsDtoResponse>();

            CreateMap<VendorDetail, UpdateVendorDetailsDtoRequest>();
            CreateMap<UpdateVendorDetailsDtoRequest, VendorDetail>();

            CreateMap<Region, VendorDetailsRegionDto>();
            CreateMap<VendorDetailsRegionDto, Region>();

            CreateMap<Country, VendorDetailsCountryDto>();
            CreateMap<VendorDetailsCountryDto, Country>();

            CreateMap<VendorAddress, GetVendorAddressDtoResponse>();
            CreateMap<GetVendorAddressDtoResponse, VendorAddress>();

            CreateMap<User, GetVendorUsersDtoResponse>();
            CreateMap<GetVendorUsersDtoResponse, User>();

            CreateMap<VendorBilling, GetVendorDetailsBillingDtoResponse>();
            CreateMap<GetVendorDetailsBillingDtoResponse, VendorBilling>();

            CreateMap<VendorAddress, CreateVendorAddressDto>();
            CreateMap<CreateVendorAddressDto, VendorAddress>();

            CreateMap<VendorDetail, UpdateVendorDetailsDtoRequest>();
            CreateMap<UpdateVendorDetailsDtoRequest, VendorDetail>();

            CreateMap<VendorAddress, UpdateVendorDetailsAddressDto>();
            CreateMap<UpdateVendorDetailsAddressDto, VendorAddress>();
            #endregion


            /*-------------- Vendor Billing --------------*/
            #region
            CreateMap<VendorBilling, GetVendorBillingDtoResponse>();
            CreateMap<GetVendorBillingDtoResponse, VendorBilling>();

            CreateMap<CreateVendorBillingDtoRequest, VendorBilling>();
            CreateMap<VendorBilling, CreateVendorBillingDtoRequest>();

            CreateMap<VendorBilling, UpdateVendorBillingDtoRequest>();
            CreateMap<UpdateVendorBillingDtoRequest, VendorBilling>();

            CreateMap<VendorDetail, GetVendorBillingVendorDetailsDtoResponse>();
            CreateMap<GetVendorBillingVendorDetailsDtoResponse, VendorDetail>();
            #endregion


            #region
            /*-------------- Job Details --------------*/
            CreateMap<JobDetail, CreateJobDtoRequest>();
            CreateMap<CreateJobDtoRequest, JobDetail>();            

            CreateMap<GetJobDetailsDtoResponse, JobDetail>();
            CreateMap<JobDetail, GetJobDetailsDtoResponse>();

            CreateMap<ServiceType, JobDetailsServiceTypeDto>();
            CreateMap<JobDetailsServiceTypeDto, ServiceType>();

            CreateMap<AllocatedAtClient, JobDetailsAllocatedAtClientDto>();
            CreateMap<JobDetailsAllocatedAtClientDto, AllocatedAtClient>();

            CreateMap<JobStatusType, JobDetailsStatusDto>();
            CreateMap<JobDetailsStatusDto, JobStatusType>();

            CreateMap<ClientDetail, JobDetailsClientDto>();
            CreateMap<JobDetailsClientDto, ClientDetail>();

            CreateMap<Department, JobDetailsDepartmentDto>();
            CreateMap<JobDetailsDepartmentDto, Department>();

            CreateMap<JobDetail, UpdateJobDetailsDtoRequest>();
            CreateMap<UpdateJobDetailsDtoRequest, JobDetail>();

            CreateMap<Region, JobDetailsRegionDto>();
            CreateMap<JobDetailsRegionDto, Region>();

            CreateMap<Country, JobDetailsCountryDto>();
            CreateMap<JobDetailsCountryDto, Country>();

            CreateMap<JobStatusType, GetJobStatusTypesDtoResponse>();
            CreateMap<GetJobStatusTypesDtoResponse, JobStatusType>();
            #endregion



            #region
            /*-------------- Job Order Details --------------*/
            CreateMap<JobOrder, CreateJobOrderDtoRequest>();
            CreateMap<CreateJobOrderDtoRequest, JobOrder>();

            CreateMap<JobOrderStatusType, GetJobOrdersStatusTypesDtoResponse>();
            CreateMap<GetJobOrdersStatusTypesDtoResponse, JobOrderStatusType>();

            CreateMap<GetJobOrdersDtoResponse, JobOrder>();
            CreateMap<JobOrder, GetJobOrdersDtoResponse>();

            CreateMap<JobOrder, UpdateJobOrderDtoRequest>();
            CreateMap<UpdateJobOrderDtoRequest, JobOrder>();

            CreateMap<VendorDetail, JobOrdersVendorDto>();
            CreateMap<JobOrdersVendorDto, VendorDetail>();

            CreateMap<JobOrderStatusType, JobOrdersStatusTypeDto>();
            CreateMap<JobOrdersStatusTypeDto, JobOrderStatusType>();

            CreateMap<WorkerDetail, JobOrdersWorkerDetailsDto>();
            CreateMap<JobOrdersWorkerDetailsDto, WorkerDetail>();
            #endregion


            /*-------------- Department --------------*/
            #region
            CreateMap<Department, GetDepartmentDtoResponse>();
            CreateMap<GetDepartmentDtoResponse, Department>();

            CreateMap<CreateDepartmentDtoRequest, Department>();
            CreateMap<Department, CreateDepartmentDtoRequest>();

            CreateMap<Department, UpdateDepartmentDtoRequest>();
            CreateMap<UpdateDepartmentDtoRequest, Department>();
            #endregion


            /*-------------- Service Types --------------*/
            #region
            CreateMap<ServiceType, GetServiceTypeDtoResponse>();
            CreateMap<GetServiceTypeDtoResponse, ServiceType>();
            #endregion

            /*-------------- Allocated At Client Types --------------*/
            #region
            CreateMap<AllocatedAtClient, GetAllocatedAtClientTypeDtoResponse>();
            CreateMap<GetAllocatedAtClientTypeDtoResponse, AllocatedAtClient>();
            #endregion

            /*-------------- Worker Details --------------*/
            #region
            CreateMap<WorkerDetail, GetWorkerDetailsDtoResponse>();
            CreateMap<GetWorkerDetailsDtoResponse, WorkerDetail>();

            CreateMap<WorkerDetail, GetWorkerFullDetailsDtoResponse>();
            CreateMap<GetWorkerFullDetailsDtoResponse, WorkerDetail>();

            CreateMap<WorkerDetail, CreateWorkerDetailsDtoRequest>();
            CreateMap<CreateWorkerDetailsDtoRequest, WorkerDetail>();

            CreateMap<WorkerDetail, UpdateWorkerDetailsDtoRequest>();
            CreateMap<UpdateWorkerDetailsDtoRequest, WorkerDetail>();

            CreateMap<WorkerStatusTypes, GetWorkerDetailsStatusType>();
            CreateMap<GetWorkerDetailsStatusType, WorkerStatusTypes>();

            CreateMap<WorkerContractStatusTypes, GetWorkerDetailsContractStatusType>();
            CreateMap<GetWorkerDetailsContractStatusType, WorkerContractStatusTypes>();

            CreateMap<ClientDetail, GetWorkerClientDetails>();
            CreateMap<GetWorkerClientDetails, ClientDetail>();

            CreateMap<VendorDetail, GetWorkerVendorDetails>();
            CreateMap<GetWorkerVendorDetails, VendorDetail>();

            CreateMap<WorkerEquipmentCost, GetWorkerDetailsEquipmentCostDto>();
            CreateMap<GetWorkerDetailsEquipmentCostDto, WorkerEquipmentCost>();

            CreateMap<JobDetail, GetWorkerFullDetailsJobDetailsDto>();
            CreateMap<GetWorkerFullDetailsJobDetailsDto, JobDetail>();

            CreateMap<JobOrder, GetWorkerFullDetailsJobOrderDetailsDto>();
            CreateMap<GetWorkerFullDetailsJobOrderDetailsDto, JobOrder>();

            CreateMap<Department, GetWorkerFullDetailsDepartmentDto>();
            CreateMap<GetWorkerFullDetailsDepartmentDto, Department>();

            CreateMap<AllocatedAtClient, GetWorkerFullDetailsAllocatedAtClientDto>();
            CreateMap<GetWorkerFullDetailsAllocatedAtClientDto, AllocatedAtClient>();

            CreateMap<User, GetWorkerFullDetailsHiringManagerDetailsDto>();
            CreateMap<GetWorkerFullDetailsHiringManagerDetailsDto, User>();

            CreateMap<User, GetWorkerFullDetailsAccountManagerDetailsDto>();
            CreateMap<GetWorkerFullDetailsAccountManagerDetailsDto, User>();
            #endregion

            /*-------------- Worker Equipment Cost --------------*/
            #region
            CreateMap<WorkerEquipmentCost, GetWorkerEquipmentCostDetailsDtoResponse>();
            CreateMap<GetWorkerEquipmentCostDetailsDtoResponse, WorkerEquipmentCost>();

            CreateMap<WorkerEquipmentCost, CreateWorkerEquipmentCostDetailsDtoRequest>();
            CreateMap<CreateWorkerEquipmentCostDetailsDtoRequest, WorkerEquipmentCost>();

            CreateMap<WorkerEquipmentCost, UpdateWorkerEquipmentCostDetailsDtoRequest>();
            CreateMap<UpdateWorkerEquipmentCostDetailsDtoRequest, WorkerEquipmentCost>();
            #endregion

            /*-------------- Worker Evaluation --------------*/
            #region
            CreateMap<WorkerEvaluation, GetWorkerEvaluationDetailsDtoResponse>();
            CreateMap<GetWorkerEvaluationDetailsDtoResponse, WorkerEvaluation>();

            CreateMap<WorkerEvaluation, CreateWorkerEvaluationDetailsDtoRequest>();
            CreateMap<CreateWorkerEvaluationDetailsDtoRequest, WorkerEvaluation>();

            CreateMap<WorkerEvaluation, UpdateWorkerEvaluationDetailsDtoRequest>();
            CreateMap<UpdateWorkerEvaluationDetailsDtoRequest, WorkerEvaluation>();
            #endregion

            /*-------------- Worker Background Evaluation --------------*/
            #region
            CreateMap<WorkerBackgroundEvaluation, GetWorkerBckgEvaluationDetailsDtoResponse>();
            CreateMap<GetWorkerBckgEvaluationDetailsDtoResponse, WorkerBackgroundEvaluation>();

            CreateMap<WorkerBackgroundEvaluation, CreateWorkerBckgEvaluationDetailsDtoRequest>();
            CreateMap<CreateWorkerBckgEvaluationDetailsDtoRequest, WorkerBackgroundEvaluation>();

            CreateMap<WorkerBackgroundEvaluation, UpdateWorkerBckgEvaluationDetailsDtoRequest>();
            CreateMap<UpdateWorkerBckgEvaluationDetailsDtoRequest, WorkerBackgroundEvaluation>();

            CreateMap<WorkerBckgEvalValidationTypes, GetWorkerBckgEvaluationValidationTypesDtoResponse>();
            CreateMap<GetWorkerBckgEvaluationValidationTypesDtoResponse, WorkerBckgEvalValidationTypes>();
            #endregion


            /*-------------- Worker Extension --------------*/
            #region
            CreateMap<WorkerExtension, GetWorkerExtensionDetailsDtoResponse>();
            CreateMap<GetWorkerExtensionDetailsDtoResponse, WorkerExtension>();

            CreateMap<WorkerExtension, CreateWorkerExtensionDetailsDtoRequest>();
            CreateMap<CreateWorkerExtensionDetailsDtoRequest, WorkerExtension>();

            CreateMap<WorkerExtension, UpdateWorkerExtensionDetailsDtoRequest>();
            CreateMap<UpdateWorkerExtensionDetailsDtoRequest, WorkerExtension>();

            CreateMap<WorkerExtensionStatusTypes, GeWorkerExtensionDetailsStatusTypesDtoResponse>();
            CreateMap<GeWorkerExtensionDetailsStatusTypesDtoResponse, WorkerExtensionStatusTypes>();
            #endregion


            /*-------------- Dashboard --------------*/
            #region
            CreateMap<GetWorkerCountPerDepartment, GetWorkerCountPerDepartmentSPDto>();
            CreateMap<GetWorkerCountPerDepartmentSPDto, GetWorkerCountPerDepartment>();
            #endregion
        }
    }
}
