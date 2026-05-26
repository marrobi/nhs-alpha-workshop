using System.Text.Json;
using ImmForm.Web.Models;

namespace ImmForm.Web.Services;

public interface ISessionService
{
    RegistrationSession GetRegistrationSession(ISession session);
    void SaveRegistrationSession(ISession session, RegistrationSession data);
    void ClearRegistrationSession(ISession session);
}

public class SessionService : ISessionService
{
    private const string SessionKey = "RegistrationSession";

    public RegistrationSession GetRegistrationSession(ISession session)
    {
        var json = session.GetString(SessionKey);
        if (string.IsNullOrEmpty(json))
            return new RegistrationSession();

        return JsonSerializer.Deserialize<RegistrationSession>(json) ?? new RegistrationSession();
    }

    public void SaveRegistrationSession(ISession session, RegistrationSession data)
    {
        var json = JsonSerializer.Serialize(data);
        session.SetString(SessionKey, json);
    }

    public void ClearRegistrationSession(ISession session)
    {
        session.Remove(SessionKey);
    }
}
