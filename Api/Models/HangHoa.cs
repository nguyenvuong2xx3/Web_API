﻿using System;

namespace Api.Models
{
    public class HangHoaVM
    {
        public string TenHangHoa {  get; set; }
        public double DonGia { get; set; }
    }
    public class HangHoa : HangHoaVM
    {
        public Guid MaHangHoa { get; set; }
    }
    public class HangHoaModel
    {
        public Guid MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public double DonGia { get; set; }
        public string TenLoai {  get; set; }
    }

}
