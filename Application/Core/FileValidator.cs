//using System;
//using FluentValidation;
//using Microsoft.AspNetCore.Http;

//namespace Application.Core
//{
//    public class FileAudioValidator : AbstractValidator<IFormFile>
//    {
//        public FileAudioValidator()
//        {
//            RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(100000000)
//                .WithMessage("File size is larger than allowed");

//            RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png") || x.Equals("audio/wav") || x.Equals("audio/wave") || x.Equals("audio/mpeg"))
//                .WithMessage("File type is not allowed");
//        }
//    }
//}




