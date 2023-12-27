﻿using System.ComponentModel.DataAnnotations;

namespace Proje_B201210567.Models
{
	public class Admin
	{
		[Key]
        public int Id { get; set; }
        public string Tc { get; set; }
        public string Adi { get; set; }
        public string Soyad { get; set; }
        public string TelefonNumarasi { get; set; }
        public string Email {  get; set; }
        public string Sifre { get; set; }
    }
}
