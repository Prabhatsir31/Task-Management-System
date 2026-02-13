using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.ResponseModel;

namespace TaskManagementSystem.Repository.Interface
{
    public interface IApplicationConfiguration
    {
        Task<ApplicationConfigurationResponse> GetApplicationConfigByKey(string key);
        Task<List<ApplicationConfigurationResponse>> GetAllActiveApplicationConfigs();
        Task<List<ApplicationConfigurationResponse>> GetAllApplicationConfigs();
        Task<string> GetParameterValue(string Key);
        Task GenerateAppConfig(string Key, string Value);
        string UserNavDesignConfig();
    }
}
