﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TP.Helpers {
  class PlaceMap {
    private Dictionary<int, int> placeMap = new Dictionary<int, int>();

    public PlaceMap() {
      initValues();
    }

    public int GetPlace(int planning) {
      int place;
      if (!placeMap.TryGetValue(planning, out place)) {
        throw new Exception(string.Format("Planning ID {0} not valid", planning));
      }
      return place;
    }

    private void initValues() {
      placeMap.Add(1001, 1);
      placeMap.Add(2001, 3);
      placeMap.Add(2002, 2);
      placeMap.Add(3001, 7);
      placeMap.Add(3002, 5);
      placeMap.Add(3003, 4);
      placeMap.Add(3004, 6);
      placeMap.Add(4001, 15);
      placeMap.Add(4002, 11);
      placeMap.Add(4003, 13);
      placeMap.Add(4004, 9);
      placeMap.Add(4005, 8);
      placeMap.Add(4006, 12);
      placeMap.Add(4007, 10);
      placeMap.Add(4008, 14);
      placeMap.Add(5001, 31);
      placeMap.Add(5002, 23);
      placeMap.Add(5003, 27);
      placeMap.Add(5004, 19);
      placeMap.Add(5005, 29);
      placeMap.Add(5006, 21);
      placeMap.Add(5007, 25);
      placeMap.Add(5008, 17);
      placeMap.Add(5009, 16);
      placeMap.Add(5010, 24);
      placeMap.Add(5011, 20);
      placeMap.Add(5012, 28);
      placeMap.Add(5013, 18);
      placeMap.Add(5014, 26);
      placeMap.Add(5015, 22);
      placeMap.Add(5016, 30);
      placeMap.Add(6001, 63);
      placeMap.Add(6002, 47);
      placeMap.Add(6003, 55);
      placeMap.Add(6004, 39);
      placeMap.Add(6005, 59);
      placeMap.Add(6006, 43);
      placeMap.Add(6007, 51);
      placeMap.Add(6008, 35);
      placeMap.Add(6009, 61);
      placeMap.Add(6010, 45);
      placeMap.Add(6011, 53);
      placeMap.Add(6012, 37);
      placeMap.Add(6013, 57);
      placeMap.Add(6014, 41);
      placeMap.Add(6015, 49);
      placeMap.Add(6016, 33);
      placeMap.Add(6017, 32);
      placeMap.Add(6018, 48);
      placeMap.Add(6019, 40);
      placeMap.Add(6020, 56);
      placeMap.Add(6021, 36);
      placeMap.Add(6022, 52);
      placeMap.Add(6023, 44);
      placeMap.Add(6024, 60);
      placeMap.Add(6025, 34);
      placeMap.Add(6026, 50);
      placeMap.Add(6027, 42);
      placeMap.Add(6028, 58);
      placeMap.Add(6029, 38);
      placeMap.Add(6030, 54);
      placeMap.Add(6031, 46);
      placeMap.Add(6032, 62);
      placeMap.Add(7001, 127);
      placeMap.Add(7002, 95);
      placeMap.Add(7003, 111);
      placeMap.Add(7004, 79);
      placeMap.Add(7005, 119);
      placeMap.Add(7006, 87);
      placeMap.Add(7007, 103);
      placeMap.Add(7008, 71);
      placeMap.Add(7009, 123);
      placeMap.Add(7010, 91);
      placeMap.Add(7011, 107);
      placeMap.Add(7012, 75);
      placeMap.Add(7013, 115);
      placeMap.Add(7014, 83);
      placeMap.Add(7015, 99);
      placeMap.Add(7016, 67);
      placeMap.Add(7017, 125);
      placeMap.Add(7018, 93);
      placeMap.Add(7019, 109);
      placeMap.Add(7020, 77);
      placeMap.Add(7021, 117);
      placeMap.Add(7022, 85);
      placeMap.Add(7023, 101);
      placeMap.Add(7024, 69);
      placeMap.Add(7025, 121);
      placeMap.Add(7026, 89);
      placeMap.Add(7027, 105);
      placeMap.Add(7028, 73);
      placeMap.Add(7029, 113);
      placeMap.Add(7030, 81);
      placeMap.Add(7031, 97);
      placeMap.Add(7032, 65);
      placeMap.Add(7033, 64);
      placeMap.Add(7034, 96);
      placeMap.Add(7035, 80);
      placeMap.Add(7036, 112);
      placeMap.Add(7037, 72);
      placeMap.Add(7038, 104);
      placeMap.Add(7039, 88);
      placeMap.Add(7040, 120);
      placeMap.Add(7041, 68);
      placeMap.Add(7042, 100);
      placeMap.Add(7043, 84);
      placeMap.Add(7044, 116);
      placeMap.Add(7045, 76);
      placeMap.Add(7046, 108);
      placeMap.Add(7047, 92);
      placeMap.Add(7048, 124);
      placeMap.Add(7049, 66);
      placeMap.Add(7050, 98);
      placeMap.Add(7051, 82);
      placeMap.Add(7052, 114);
      placeMap.Add(7053, 74);
      placeMap.Add(7054, 106);
      placeMap.Add(7055, 90);
      placeMap.Add(7056, 122);
      placeMap.Add(7057, 70);
      placeMap.Add(7058, 102);
      placeMap.Add(7059, 86);
      placeMap.Add(7060, 118);
      placeMap.Add(7061, 78);
      placeMap.Add(7062, 110);
      placeMap.Add(7063, 94);
      placeMap.Add(7064, 126);
      placeMap.Add(8001, 255);
      placeMap.Add(8002, 191);
      placeMap.Add(8003, 223);
      placeMap.Add(8004, 159);
      placeMap.Add(8005, 239);
      placeMap.Add(8006, 175);
      placeMap.Add(8007, 207);
      placeMap.Add(8008, 143);
      placeMap.Add(8009, 247);
      placeMap.Add(8010, 183);
      placeMap.Add(8011, 215);
      placeMap.Add(8012, 151);
      placeMap.Add(8013, 231);
      placeMap.Add(8014, 167);
      placeMap.Add(8015, 199);
      placeMap.Add(8016, 135);
      placeMap.Add(8017, 251);
      placeMap.Add(8018, 187);
      placeMap.Add(8019, 219);
      placeMap.Add(8020, 155);
      placeMap.Add(8021, 235);
      placeMap.Add(8022, 171);
      placeMap.Add(8023, 203);
      placeMap.Add(8024, 139);
      placeMap.Add(8025, 243);
      placeMap.Add(8026, 179);
      placeMap.Add(8027, 211);
      placeMap.Add(8028, 147);
      placeMap.Add(8029, 227);
      placeMap.Add(8030, 163);
      placeMap.Add(8031, 195);
      placeMap.Add(8032, 131);
      placeMap.Add(8033, 253);
      placeMap.Add(8034, 189);
      placeMap.Add(8035, 221);
      placeMap.Add(8036, 157);
      placeMap.Add(8037, 237);
      placeMap.Add(8038, 173);
      placeMap.Add(8039, 205);
      placeMap.Add(8040, 141);
      placeMap.Add(8041, 245);
      placeMap.Add(8042, 181);
      placeMap.Add(8043, 213);
      placeMap.Add(8044, 149);
      placeMap.Add(8045, 229);
      placeMap.Add(8046, 165);
      placeMap.Add(8047, 197);
      placeMap.Add(8048, 133);
      placeMap.Add(8049, 249);
      placeMap.Add(8050, 185);
      placeMap.Add(8051, 217);
      placeMap.Add(8052, 153);
      placeMap.Add(8053, 233);
      placeMap.Add(8054, 169);
      placeMap.Add(8055, 201);
      placeMap.Add(8056, 137);
      placeMap.Add(8057, 241);
      placeMap.Add(8058, 177);
      placeMap.Add(8059, 209);
      placeMap.Add(8060, 145);
      placeMap.Add(8061, 225);
      placeMap.Add(8062, 161);
      placeMap.Add(8063, 193);
      placeMap.Add(8064, 129);
      placeMap.Add(8065, 128);
      placeMap.Add(8066, 192);
      placeMap.Add(8067, 160);
      placeMap.Add(8068, 224);
      placeMap.Add(8069, 144);
      placeMap.Add(8070, 208);
      placeMap.Add(8071, 176);
      placeMap.Add(8072, 240);
      placeMap.Add(8073, 136);
      placeMap.Add(8074, 200);
      placeMap.Add(8075, 168);
      placeMap.Add(8076, 232);
      placeMap.Add(8077, 152);
      placeMap.Add(8078, 216);
      placeMap.Add(8079, 184);
      placeMap.Add(8080, 248);
      placeMap.Add(8081, 132);
      placeMap.Add(8082, 196);
      placeMap.Add(8083, 164);
      placeMap.Add(8084, 228);
      placeMap.Add(8085, 148);
      placeMap.Add(8086, 212);
      placeMap.Add(8087, 180);
      placeMap.Add(8088, 244);
      placeMap.Add(8089, 140);
      placeMap.Add(8090, 204);
      placeMap.Add(8091, 172);
      placeMap.Add(8092, 236);
      placeMap.Add(8093, 156);
      placeMap.Add(8094, 220);
      placeMap.Add(8095, 188);
      placeMap.Add(8096, 252);
      placeMap.Add(8097, 130);
      placeMap.Add(8098, 194);
      placeMap.Add(8099, 162);
      placeMap.Add(8100, 226);
      placeMap.Add(8101, 146);
      placeMap.Add(8102, 210);
      placeMap.Add(8103, 178);
      placeMap.Add(8104, 242);
      placeMap.Add(8105, 138);
      placeMap.Add(8106, 202);
      placeMap.Add(8107, 170);
      placeMap.Add(8108, 234);
      placeMap.Add(8109, 154);
      placeMap.Add(8110, 218);
      placeMap.Add(8111, 186);
      placeMap.Add(8112, 250);
      placeMap.Add(8113, 134);
      placeMap.Add(8114, 198);
      placeMap.Add(8115, 166);
      placeMap.Add(8116, 230);
      placeMap.Add(8117, 150);
      placeMap.Add(8118, 214);
      placeMap.Add(8119, 182);
      placeMap.Add(8120, 246);
      placeMap.Add(8121, 142);
      placeMap.Add(8122, 206);
      placeMap.Add(8123, 174);
      placeMap.Add(8124, 238);
      placeMap.Add(8125, 158);
      placeMap.Add(8126, 222);
      placeMap.Add(8127, 190);
      placeMap.Add(8128, 254);
    }
  }
}