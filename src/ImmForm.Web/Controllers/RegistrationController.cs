using ImmForm.Web.Models;
using ImmForm.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImmForm.Web.Controllers;

public class RegistrationController : Controller
{
    private readonly ISessionService _sessionService;
    private readonly IOrganisationApiClient _organisationApiClient;
    private readonly IRegistrationService _registrationService;

    public RegistrationController(
        ISessionService sessionService,
        IOrganisationApiClient organisationApiClient,
        IRegistrationService registrationService)
    {
        _sessionService = sessionService;
        _organisationApiClient = organisationApiClient;
        _registrationService = registrationService;
    }

    [HttpGet("/register/applicant-details")]
    public IActionResult ApplicantDetails([FromQuery] string? returnTo)
    {
        var session = _sessionService.GetRegistrationSession(HttpContext.Session);

        if (returnTo == "check-your-answers")
        {
            session.ReturnToCheckYourAnswers = true;
            _sessionService.SaveRegistrationSession(HttpContext.Session, session);
        }

        var model = new ApplicantDetailsViewModel
        {
            FirstName = session.FirstName,
            Surname = session.Surname,
            JobTitle = session.JobTitle,
            Telephone = session.Telephone,
            Email = session.Email
        };

        return View(model);
    }

    [HttpPost("/register/applicant-details")]
    [ValidateAntiForgeryToken]
    public IActionResult ApplicantDetails(ApplicantDetailsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Error: Your details";
            return View(model);
        }

        var session = _sessionService.GetRegistrationSession(HttpContext.Session);
        session.FirstName = model.FirstName?.Trim();
        session.Surname = model.Surname?.Trim();
        session.JobTitle = model.JobTitle?.Trim();
        session.Telephone = model.Telephone?.Trim();
        session.Email = model.Email?.Trim();
        _sessionService.SaveRegistrationSession(HttpContext.Session, session);

        if (session.ReturnToCheckYourAnswers)
        {
            session.ReturnToCheckYourAnswers = false;
            _sessionService.SaveRegistrationSession(HttpContext.Session, session);
            return RedirectToAction(nameof(CheckYourAnswers));
        }

        return Redirect("/register/organisation-account");
    }

    [HttpGet("/register/organisation-account")]
    public IActionResult OrganisationAccount([FromQuery] string? returnTo)
    {
        var session = _sessionService.GetRegistrationSession(HttpContext.Session);

        if (returnTo == "check-your-answers")
        {
            session.ReturnToCheckYourAnswers = true;
            _sessionService.SaveRegistrationSession(HttpContext.Session, session);
        }

        var model = new OrganisationAccountViewModel
        {
            AccountNumber = session.AccountNumber,
            OrganisationCode = session.OrganisationCode,
            OrganisationName = session.OrganisationName
        };

        return View(model);
    }

    [HttpPost("/register/organisation-account")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OrganisationAccount(OrganisationAccountViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Error: Organisation and account";
            return View(model);
        }

        var validationResult = await _organisationApiClient.ValidateAsync(
            model.AccountNumber!.Trim(),
            model.OrganisationCode!.Trim(),
            cancellationToken);

        if (!validationResult.IsValid)
        {
            ModelState.AddModelError(nameof(model.AccountNumber), validationResult.ErrorMessage!);
            model.ApiErrorMessage = validationResult.ErrorMessage;
            ViewData["Title"] = "Error: Organisation and account";
            return View(model);
        }

        var session = _sessionService.GetRegistrationSession(HttpContext.Session);
        session.AccountNumber = model.AccountNumber.Trim();
        session.OrganisationCode = model.OrganisationCode!.Trim();
        session.OrganisationName = validationResult.OrganisationName;
        session.AuthorisedPersonName = validationResult.AuthorisedPersonName;
        session.AuthorisedPersonEmail = validationResult.AuthorisedPersonEmail;
        _sessionService.SaveRegistrationSession(HttpContext.Session, session);

        if (session.ReturnToCheckYourAnswers)
        {
            session.ReturnToCheckYourAnswers = false;
            _sessionService.SaveRegistrationSession(HttpContext.Session, session);
            return RedirectToAction(nameof(CheckYourAnswers));
        }

        return Redirect("/register/check-your-answers");
    }

    [HttpGet("/register/check-your-answers")]
    public IActionResult CheckYourAnswers()
    {
        var session = _sessionService.GetRegistrationSession(HttpContext.Session);

        if (session.IsSubmitted && !string.IsNullOrEmpty(session.CorrelationId))
        {
            return Redirect($"/register/confirmation/{session.CorrelationId}");
        }

        if (string.IsNullOrEmpty(session.FirstName) || string.IsNullOrEmpty(session.AccountNumber))
        {
            return Redirect("/");
        }

        var model = new CheckYourAnswersViewModel
        {
            FirstName = session.FirstName,
            Surname = session.Surname,
            JobTitle = session.JobTitle,
            Telephone = session.Telephone,
            Email = session.Email,
            AccountNumber = session.AccountNumber,
            OrganisationCode = session.OrganisationCode,
            OrganisationName = session.OrganisationName
        };

        return View(model);
    }

    [HttpGet("/register/declaration")]
    public IActionResult Declaration()
    {
        var session = _sessionService.GetRegistrationSession(HttpContext.Session);

        if (session.IsSubmitted && !string.IsNullOrEmpty(session.CorrelationId))
        {
            return Redirect($"/register/confirmation/{session.CorrelationId}");
        }

        if (string.IsNullOrEmpty(session.FirstName) || string.IsNullOrEmpty(session.AccountNumber))
        {
            return Redirect("/");
        }

        var model = new DeclarationViewModel
        {
            FullName = $"{session.FirstName} {session.Surname}",
            DeclarationJobTitle = session.JobTitle
        };

        return View(model);
    }

    [HttpPost("/register/declaration")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Declaration(DeclarationViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Error: Declaration";
            return View(model);
        }

        var session = _sessionService.GetRegistrationSession(HttpContext.Session);

        if (session.IsSubmitted && !string.IsNullOrEmpty(session.CorrelationId))
        {
            return Redirect($"/register/confirmation/{session.CorrelationId}");
        }

        var correlationId = await _registrationService.SubmitRegistrationAsync(
            session,
            model.FullName!.Trim(),
            model.DeclarationJobTitle!.Trim(),
            cancellationToken);

        session.IsSubmitted = true;
        session.CorrelationId = correlationId;
        _sessionService.SaveRegistrationSession(HttpContext.Session, session);

        return Redirect($"/register/confirmation/{correlationId}");
    }

    [HttpGet("/register/confirmation/{correlationId}")]
    public IActionResult Confirmation(string correlationId)
    {
        var model = new ConfirmationViewModel
        {
            CorrelationId = correlationId
        };

        return View(model);
    }
}
