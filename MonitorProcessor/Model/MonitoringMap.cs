using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitorProcessor.ProjectEnums;

namespace MonitorProcessor.Model
{
    public class MonitoringMap
    {
        
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public ProtocolType ProtocolType { get; set; }
        public string? HeartRateMap { get; set; }
        public string? PVCMap { get; set; }
        public string? ST_IMap { get; set; }
        public string? ST_IIMap { get; set; }
        public string? ST_IIIMap { get; set; }
        public string? ST_AVRMap { get; set; }
        public string? ST_AVFMap { get; set; }
        public string? ST_AVLMap { get; set; }
        public string? ST_AV1Map { get; set; }
        public string? ST_AV2Map { get; set; }
        public string? ST_AV3Map { get; set; }
        public string? ST_AV4Map { get; set; }
        public string? ST_AV5Map { get; set; }
        public string? ST_AV6Map { get; set; }
        public string? T1Map { get; set; }
        public string? T2Map { get; set; }
        public string? DTMap { get; set; }
        public string? FRMap { get; set; }
        public string? SPO2Map { get; set; }
        public string? SPO2_PRMap { get; set; }
        public string? SPO2_PIMap { get; set; }
        public string? PI_SISTMap { get; set; }
        public string? PI_DIASMap { get; set; }
        public string? PI_MEDMap { get; set; }
        public string? PI_PRMap { get; set; }
        public string? PNI_SISTMap { get; set; }
        public string? PNI_DIASMap { get; set; }
        public string? PNI_MEDMap { get; set; }
        public string? PNI_PRMap { get; set; }
        public string? CO2Map { get; set; }
        public string? ETCO2Map { get; set; }
        public string? INCO2Map { get; set; }
        public string? WeightMap { get; set; }
        public string? HeightMap { get; set; }
    }
}
