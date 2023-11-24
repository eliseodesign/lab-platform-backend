using LabPlatform.Models.DTOs;

namespace LabPlatform.Services.Interfaces;

public interface IEmailService
{
  bool SendEmail(EmailDTO emailDTO);
}
