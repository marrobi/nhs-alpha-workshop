using System.Net.Http.Json;

namespace ImmForm.Web.Services;

public record OrganisationValidationResult(
    bool IsValid,
    string? OrganisationName,
    string? AuthorisedPersonName,
    string? AuthorisedPersonEmail,
    string? ErrorMessage);

public interface IOrganisationApiClient
{
    Task<OrganisationValidationResult> ValidateAsync(string accountNumber, string organisationCode, CancellationToken cancellationToken);
}

public class OrganisationApiClient : IOrganisationApiClient
{
    private readonly HttpClient _httpClient;

    public OrganisationApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<OrganisationValidationResult> ValidateAsync(string accountNumber, string organisationCode, CancellationToken cancellationToken)
    {
        try
        {
            var request = new { AccountNumber = accountNumber, OrganisationCode = organisationCode };
            var response = await _httpClient.PostAsJsonAsync("/api/mock/organisation/validate", request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return new OrganisationValidationResult(
                    false, null, null, null,
                    "The validation service is temporarily unavailable. Try again in a few minutes or contact the ImmForm helpdesk at helpdesk@immform.org.uk.");
            }

            var result = await response.Content.ReadFromJsonAsync<OrganisationApiResponse>(cancellationToken);

            if (result is null || !result.IsValid)
            {
                return new OrganisationValidationResult(
                    false, null, null, null,
                    "We could not find this account and organisation code combination in ImmForm. Check your ImmForm account number and organisation code are correct, or contact the ImmForm helpdesk at helpdesk@immform.org.uk.");
            }

            if (string.IsNullOrWhiteSpace(result.AuthorisedPersonEmail))
            {
                return new OrganisationValidationResult(
                    false, result.OrganisationName, null, null,
                    "We cannot find an Authorised Person for this account. Check your ImmForm account number and organisation code are correct, or contact the ImmForm helpdesk at helpdesk@immform.org.uk.");
            }

            return new OrganisationValidationResult(
                true,
                result.OrganisationName,
                result.AuthorisedPersonName,
                result.AuthorisedPersonEmail,
                null);
        }
        catch (TaskCanceledException)
        {
            return new OrganisationValidationResult(
                false, null, null, null,
                "The validation service is temporarily unavailable. Try again in a few minutes or contact the ImmForm helpdesk at helpdesk@immform.org.uk.");
        }
        catch (HttpRequestException)
        {
            return new OrganisationValidationResult(
                false, null, null, null,
                "The validation service is temporarily unavailable. Try again in a few minutes or contact the ImmForm helpdesk at helpdesk@immform.org.uk.");
        }
    }

    private record OrganisationApiResponse(
        bool IsValid,
        string? OrganisationName,
        string? AuthorisedPersonName,
        string? AuthorisedPersonEmail);
}
