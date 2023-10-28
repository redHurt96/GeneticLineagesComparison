using System.Collections.Generic;
using System.Linq;

namespace LineagesComparison.Calculation
{
    internal static class SamplesNamesParser
    {
        public static Dictionary<string, string[]> Parse(string rawInput)
        {
            return new Dictionary<string, string[]>()
            {
                ["L0"] = ParseToSamplesArray("ArmAMD81 TseyAMD1 TseyAMD4 TseyAMD6 TseyAMD7 TseyAMD8 TseyAMD11 TseyAMD13 WCaSAV251 WCaSAV256"),
                ["L1"] = ParseToSamplesArray("KBRAMD25 ArmAMD77 ArmAMD78 ArmAMD79 CriAMD123 CriAMD119 CriAMD120 CriAMD122 CriAMD111 CriAMD112 GeoAMD35 GeoAMD31 GeoAMD39 GeoAMD40 GeoAMD41 KarSAV301 KazSDA50 KazSDA51 KazSDA40 KazSDA41 KazSDA60 StKrAMD149 StKrAMD151 StKrAMD150 ASTRAMD160 ASTRAMD159 ASTRAMD161 DAGAMD171 DAGAMD169 DAGAMD170 DAGAMD175 DAGAMD176 DAGAMD179 DAGAMD180 DAGAMD181 DAGAMD183 DAGAMD182 RostAMD187 RostAMD189 RostAMD186 RostAMD190 RostAMD191 RostAMD192 StavAMD195 KrKrAMD200 KrKrAMD202 KrKrAMD203 WCaSAV277 HovSAV21 HovSAV22 VorSAV67 HovSAV23 OrehSAV10 OrehSAV9 OrehSAV26 HovSAV24 PrVeSAV18 HovSAV20 ArmAMD83 CriAMD129 NiNoSDA181 NiNoSDA182 NiNoSDA183 NiNoSDA184 SocSAV388 SocSAV389 SocSAV395 ASTRAMD164"),
                ["L2"] = ParseToSamplesArray("BelAMD209 BelAMD205 BelAMD206 BelAMD207 KarSAV305 KarSAV314 KarSAV315 KarSAV336 KarSAV338 VladAMD213 BalSAV181 BalSAV182 ShakSAV95 ShakSAV105 VDNHSAV12 BalSAV180 BitSAV172 CriAMD115 KalSDA188 KalSDA189"),
                ["L3"] = ParseToSamplesArray("CriAMD131 AthSAV226 AthSAV227 CriAMD133"),
                ["L4"] = ParseToSamplesArray("KBRAMD24 KarSAV303 WCaSAV252 WCaSAV253 WCaSAV272 WCaSAV275 LosiSAV206 BitSAV167 ShakSAV114 ShakSAV116 ShakSAV115 VorSAV66 VorSAV70 KalSDA186 RznSDA191 RznSDA192 RznSDA193 VladAMD177 VladAMD178 VladAMD179"),
                ["L5"] = ParseToSamplesArray("AzeAMD19 AzeAMD20 AzeAMD22 AzeAMD23 AzeAMD21 Iran"),
                ["L6"] = ParseToSamplesArray("CriAMD89 CriAMD91 CriAMD92 CriAMD90"),
                ["L7"] = ParseToSamplesArray("KBRAMD27 KBRAMD28 KBRAMD30 KBRAMD29"),
                ["L8"] = ParseToSamplesArray("GeoAMD32 GeoAMD34 GeoAMD38"),
                ["L9"] = ParseToSamplesArray("SocSAV406 SocSAV407 SocSAV408"),
                ["L10"] = ParseToSamplesArray("Parisotoma_notabilis_isolate_HR_Pn1_cytochrome_c_oxidase_subunit_I"),
            };

            string[] ParseToSamplesArray(string input) =>
                input
                .Split(new char[] { ' ' })
                .ToArray();
        }
    }
}
