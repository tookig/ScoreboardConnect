﻿using ScoreboardLiveApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TP.Static {
  internal static class Countries {
    private static Dictionary<string, string>? s_lookupTable;
    private static Dictionary<int, string>? i_lookupTable;

    public static string GetCountryName(string code) {
      if (s_lookupTable == null) {
        Init();
      }

      if (s_lookupTable.TryGetValue(code, out string? name)) {
        return name;
      }

      return string.Empty;
    }

    public static string GetCountryName(int code) {
      if (i_lookupTable == null) {
        Init();
      }

      if (i_lookupTable.TryGetValue(code, out string? name)) {
        return name;
      }

      return string.Empty;
    }

    private static void Init() {
      s_lookupTable = new Dictionary<string, string> {
          {"AFG", "Afghanistan"},
          {"AHO", "Netherlands Antilles"},
          {"ALB", "Albania"},
          {"ALG", "Algeria"},
          {"AND", "Andorra"},
          {"ANG", "Angola"},
          {"ANT", "Antigua and Barbuda"},
          {"ARG", "Argentina"},
          {"ARM", "Armenia"},
          {"ARU", "Aruba"},
          {"ASA", "American Samoa"},
          {"AUS", "Australia"},
          {"AUT", "Austria"},
          {"AZE", "Azerbaijan"},
          {"BAH", "Bahamas"},
          {"BAN", "Bangladesh"},
          {"BAR", "Barbados"},
          {"BDI", "Burundi"},
          {"BEL", "Belgium"},
          {"BEN", "Benin"},
          {"BER", "Bermuda"},
          {"BHU", "Bhutan"},
          {"BIH", "Bosnia and Herzegovina"},
          {"BIZ", "Belize"},
          {"BLR", "Belarus"},
          {"BOL", "Bolivia"},
          {"BOT", "Botswana"},
          {"BRA", "Brazil"},
          {"BRN", "Bahrain"},
          {"BRU", "Brunei Darussalem"},
          {"BUL", "Bulgaria"},
          {"BUR", "Burkina Faso"},
          {"CAF", "Central African Republic"},
          {"CAM", "Cambodia"},
          {"CAN", "Canada"},
          {"CAY", "Cayman Islands"},
          {"CGO", "Congo"},
          {"CHA", "Chad"},
          {"CHI", "Chile"},
          {"CHN", "China"},
          {"CIV", "Ivory Coast"},
          {"CMR", "Cameroon"},
          {"COK", "Cook Islands"},
          {"COL", "Colombia"},
          {"COM", "Comoros"},
          {"CPV", "Cape Verde"},
          {"CRC", "Costa Rica"},
          {"CRO", "Croatia"},
          {"CUB", "Cuba"},
          {"CYP", "Cyprus"},
          {"CZE", "Czech Republic"},
          {"DEN", "Denmark"},
          {"DJI", "Djibouti"},
          {"DOM", "Dominican Republic"},
          {"ECU", "Ecuador"},
          {"EGY", "Egypt"},
          {"ENG", "England"},
          {"ESA", "El Salvador"},
          {"ESP", "Spain"},
          {"EST", "Estonia"},
          {"ETH", "Ethiopia"},
          {"FIJ", "Fiji"},
          {"FIN", "Finland"},
          {"FRA", "France"},
          {"GAB", "Gabon"},
          {"GAM", "Gambia"},
          {"GBR", "Great Britain"},
          {"GBS", "Guinea-Bissau"},
          {"GEO", "Georgia"},
          {"GEQ", "Equatorial Guinea"},
          {"GER", "Germany"},
          {"GHA", "Ghana"},
          {"GRE", "Greece"},
          {"GRN", "Grenada"},
          {"GUA", "Guatemala"},
          {"GUI", "Guinea"},
          {"GUM", "Guam"},
          {"GUY", "Guyana"},
          {"HAI", "Haiti"},
          {"HKG", "Hong Kong"},
          {"HON", "Honduras"},
          {"HUN", "Hungary"},
          {"INA", "Indonesia"},
          {"IND", "India"},
          {"IRI", "Iran"},
          {"IRL", "Ireland"},
          {"IRQ", "Iraq"},
          {"ISL", "Iceland"},
          {"ISR", "Israel"},
          {"ISV", "Virgin Islands"},
          {"ITA", "Italy"},
          {"IVB", "British Virgin Islands"},
          {"JAM", "Jamaica"},
          {"JOR", "Jordan"},
          {"JPN", "Japan"},
          {"KAZ", "Kazakhstan"},
          {"KEN", "Kenya"},
          {"KGZ", "Kyrgyzstan"},
          {"KIR", "Kiribati"},
          {"KOR", "Korea, Republic of"},
          {"KOS", "Kosovo"},
          {"KSA", "Saudi Arabia"},
          {"KUW", "Kuwait"},
          {"LAO", "Lao People's Democratic Republic"},
          {"LAT", "Latvia"},
          {"LBA", "Libya"},
          {"LBR", "Liberia"},
          {"LCA", "Saint Lucia"},
          {"LES", "Lesotho"},
          {"LIB", "Lebanon"},
          {"LIE", "Liechtenstein"},
          {"LTU", "Lithuania"},
          {"LUX", "Luxembourg"},
          {"MAD", "Madagascar"},
          {"MAR", "Morocco"},
          {"MAS", "Malaysia"},
          {"MAW", "Malawi"},
          {"MDA", "Moldova"},
          {"MDV", "Maldives"},
          {"MEX", "Mexico"},
          {"MGL", "Mongolia"},
          {"MHL", "Marshall Islands"},
          {"MKD", "Macedonia"},
          {"MLI", "Mali"},
          {"MLT", "Malta"},
          {"MNE", "Montenegro"},
          {"MON", "Monaco"},
          {"MOZ", "Mozambique"},
          {"MRI", "Mauritius"},
          {"MTN", "Mauritania"},
          {"MYA", "Myanmar"},
          {"NAM", "Namibia"},
          {"NCA", "Nicaragua"},
          {"NED", "Netherlands"},
          {"NEP", "Nepal"},
          {"NGR", "Nigeria"},
          {"NIG", "Niger"},
          {"NOR", "Norway"},
          {"NRU", "Nauru"},
          {"NZL", "New Zealand"},
          {"OMA", "Oman"},
          {"PAK", "Pakistan"},
          {"PAN", "Panama"},
          {"PAR", "Paraguay"},
          {"PER", "Peru"},
          {"PHI", "Philippines"},
          {"PNG", "Papua New Guinea"},
          {"POL", "Poland"},
          {"POR", "Portugal"},
          {"PRK", "Korea, Democratic People's Republic"},
          {"PUR", "Puerto Rico"},
          {"QAT", "Qatar"},
          {"ROM", "Romania"},
          {"RSA", "South Africa"},
          {"RUS", "Russia"},
          {"RWA", "Rwanda"},
          {"SAM", "Western Samoa"},
          {"SCO", "Scotland"},
          {"SEN", "Senegal"},
          {"SEY", "Seychelles"},
          {"SIN", "Singapore"},
          {"SKN", "Saint Kitts and Nevis"},
          {"SLE", "Sierra Leone"},
          {"SLO", "Slovenia"},
          {"SMR", "San Marino"},
          {"SOL", "Solomon Islands"},
          {"SOM", "Somalia"},
          {"SRI", "Sri Lanka"},
          {"STP", "Sao Tome and Principe"},
          {"SUD", "Sudan"},
          {"SUI", "Switzerland"},
          {"SUR", "Suriname"},
          {"SVK", "Slovakia"},
          {"SWE", "Sweden"},
          {"SWZ", "Eswatini"},
          {"SYR", "Syrian Arab Republic"},
          {"TAN", "Tanzania"},
          {"THA", "Thailand"},
          {"TJK", "Tajikistan"},
          {"TKM", "Turkmenistan"},
          {"TOG", "Togo"},
          {"TON", "Tonga"},
          {"TPE", "Chinese Taipei"},
          {"TRI", "Trinidad and Tobago"},
          {"TUN", "Tunisia"},
          {"TUR", "Turkey"},
          {"UAE", "United Arab Emirates"},
          {"UGA", "Uganda"},
          {"UKR", "Ukraine"},
          {"URU", "Uruguay"},
          {"USA", "United States"},
          {"UZB", "Uzbekistan"},
          {"VAN", "Vanuatu"},
          {"VEN", "Venezuela"},
          {"VIE", "Vietnam"},
          {"VIN", "Saint Vincent and the Grenadines"},
          {"WAL", "Wales"},
          {"YAM", "Yemen"},
          {"YUG", "Yugoslavia"},
          {"ZAI", "Zaire"},
          {"ZAM", "Zambia"},
          {"ZIM", "Zimbabwe"}
      };

      i_lookupTable = new Dictionary<int, string> {
        { 1 ,"Afghanistan"},
        { 2 ,"Netherlands Antilles"},
        { 3 ,"Albania"},
        { 4 ,"Algeria"},
        { 5 ,"Andorra"},
        { 6 ,"Angola"},
        { 7 ,"Antigua and Barbuda"},
        { 8 ,"Argentina"},
        { 9 ,"Armenia"},
        { 10  ,"Aruba"},
        { 11  ,"American Samoa"},
        { 12  ,"Australia"},
        { 13  ,"Austria"},
        { 14  ,"Azerbaijan"},
        { 15  ,"Bahamas"},
        { 16  ,"Bangladesh"},
        { 17  ,"Barbados"},
        { 18  ,"Burundi"},
        { 19  ,"Belgium"},
        { 20  ,"Benin"},
        { 21  ,"Bermuda"},
        { 22  ,"Bhutan"},
        { 23  ,"Bosnia and Herzegovina"},
        { 24  ,"Belize"},
        { 25  ,"Belarus"},
        { 26  ,"Bolivia"},
        { 27  ,"Botswana"},
        { 28  ,"Brazil"},
        { 29  ,"Bahrain"},
        { 30  ,"Brunei Darussalem"},
        { 31  ,"Bulgaria"},
        { 32  ,"Burkina Faso"},
        { 33  ,"Central African Republic"},
        { 34  ,"Cambodia"},
        { 35  ,"Canada"},
        { 36  ,"Cayman Islands"},
        { 37  ,"Congo"},
        { 38  ,"Chad"},
        { 39  ,"Chile"},
        { 40  ,"China"},
        { 41  ,"Ivory Coast"},
        { 42  ,"Cameroon"},
        { 43  ,"Cook Islands"},
        { 44  ,"Colombia"},
        { 45  ,"Comoros"},
        { 46  ,"Cape Verde"},
        { 47  ,"Costa Rica"},
        { 48  ,"Croatia"},
        { 49  ,"Cuba"},
        { 50  ,"Cyprus"},
        { 51  ,"Czech Republic"},
        { 52  ,"Denmark"},
        { 53  ,"Djibouti"},
        { 54  ,"Dominican Republic"},
        { 55  ,"Ecuador"},
        { 56  ,"Egypt"},
        { 57  ,"England"},
        { 58  ,"El Salvador"},
        { 59  ,"Spain"},
        { 60  ,"Estonia"},
        { 61  ,"Ethiopia"},
        { 62  ,"Fiji"},
        { 63  ,"Finland"},
        { 64  ,"France"},
        { 65  ,"Gabon"},
        { 66  ,"Gambia"},
        { 67  ,"Great Britain"},
        { 68  ,"Guinea-Bissea"},
        { 69  ,"Georgia"},
        { 70  ,"Equatorial Guinea"},
        { 71  ,"Germany"},
        { 72  ,"Ghana"},
        { 73  ,"Greece"},
        { 74  ,"Grenada"},
        { 75  ,"Guatemala"},
        { 76  ,"Guinee"},
        { 77  ,"Guam"},
        { 78  ,"Guyana"},
        { 79  ,"Haiti"},
        { 80  ,"Hong Kong"},
        { 81  ,"Honduras"},
        { 82  ,"Hungary"},
        { 83  ,"Indonesia"},
        { 84  ,"India"},
        { 85  ,"Iran"},
        { 86  ,"Ireland"},
        { 87  ,"Iraq"},
        { 88  ,"Iceland"},
        { 89  ,"Israel"},
        { 90  ,"US Virgin Islands"},
        { 91  ,"Italy"},
        { 92  ,"British Virgin Islands"},
        { 93  ,"Jamaica"},
        { 94  ,"Jordan"},
        { 95  ,"Japan"},
        { 96  ,"Kazakhstan"},
        { 97  ,"Kenya"},
        { 98  ,"Kyrgyzstan"},
        { 99  ,"Kiribati"},
        { 100 ,"Korea"},
        { 101 ,"Saudi Arabia"},
        { 102 ,"Kuwait"},
        { 103 ,"Lao"},
        { 104 ,"Latvia"},
        { 105 ,"Libya"},
        { 106 ,"Liberia"},
        { 107 ,"St. Lucia"},
        { 108 ,"Losotho"},
        { 109 ,"Lebanon"},
        { 110 ,"Liechtenstein"},
        { 111 ,"Lithuania"},
        { 112 ,"Luxembourg"},
        { 113 ,"Madagascar"},
        { 114 ,"Morocco"},
        { 115 ,"Malaysia"},
        { 116 ,"Malawi"},
        { 117 ,"Moldova"},
        { 118 ,"Maldives"},
        { 119 ,"Mexico"},
        { 120 ,"Mongolia"},
        { 121 ,"Macedonia"},
        { 122 ,"Mali"},
        { 123 ,"Malta"},
        { 124 ,"Monaco"},
        { 125 ,"Mozambique"},
        { 126 ,"Mauritius"},
        { 127 ,"Mauritania"},
        { 128 ,"Myanmar"},
        { 129 ,"Namibia"},
        { 130 ,"Nicaragua"},
        { 131 ,"Netherlands"},
        { 132 ,"Nepal"},
        { 133 ,"Nigeria"},
        { 134 ,"Niger"},
        { 135 ,"Norway"},
        { 136 ,"Nauru"},
        { 137 ,"New Zealand"},
        { 138 ,"Oman"},
        { 139 ,"Pakistan"},
        { 140 ,"Panama"},
        { 141 ,"Paraguay"},
        { 142 ,"Peru"},
        { 143 ,"Philippines"},
        { 144 ,"Papua New Guinea"},
        { 145 ,"Poland"},
        { 146 ,"Portugal"},
        { 147 ,"Korea, Dem. People's Rep.	"},
        { 148 ,"Puerto Rico"},
        { 149 ,"Qatar"},
        { 150 ,"Romania"},
        { 151 ,"South Africa"},
        { 152 ,"Russia"},
        { 153 ,"Rwanda"},
        { 154 ,"Western Samoa"},
        { 155 ,"Scotland"},
        { 156 ,"Senegal"},
        { 157 ,"Seychelles"},
        { 158 ,"Singapore"},
        { 159 ,"St. Kitts and Nevis"},
        { 160 ,"Sierra Leone"},
        { 161 ,"Slovenia"},
        { 162 ,"San Marino"},
        { 163 ,"Solomon Islands"},
        { 164 ,"Somalia"},
        { 165 ,"Sri Lanka"},
        { 166 ,"Sao Tome and Principe"},
        { 167 ,"Sudan"},
        { 168 ,"Switzerland"},
        { 169 ,"Suriname"},
        { 170 ,"Slovakia"},
        { 171 ,"Sweden"},
        { 172 ,"Swaziland"},
        { 173 ,"Syrian Arab Republic"},
        { 174 ,"Tanzania"},
        { 175 ,"Thailand"},
        { 176 ,"Tajikistan"},
        { 177 ,"Turkmenistan"},
        { 178 ,"Togo"},
        { 179 ,"Tonga"},
        { 180 ,"Chinese Taipei"},
        { 181 ,"Trinidad and Tobago"},
        { 182 ,"Tunisia"},
        { 183 ,"Turkey"},
        { 184 ,"United Arab Emirates"},
        { 185 ,"Uganda"},
        { 186 ,"Ukraine"},
        { 187 ,"Uruguay"},
        { 188 ,"U.S.A."},
        { 189 ,"Uzbekistan"},
        { 190 ,"Vanuatu"},
        { 191 ,"Venezuela"},
        { 192 ,"Vietnam"},
        { 193 ,"St. Vincent and The Grenadines"},
        { 194 ,"Wales"},
        { 195 ,"Yemen"},
        { 196 ,"Yugoslavia"},
        { 197 ,"Zaire"},
        { 198 ,"Zambia"},
        { 199 ,"Zimbabwe"}

      };
    }
  }
}