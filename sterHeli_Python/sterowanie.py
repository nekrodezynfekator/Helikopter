# -*- coding: utf-8 -*-
import serial
from Tkinter import *
import tkMessageBox
import serial.tools.list_ports
import array

class AplikacjaGUI(Frame, object):
    def __init__(self,master):
        super(AplikacjaGUI,self).__init__(master)
        self.master.title("Sterowanie helikopterem")
        self.master.geometry("450x300")
        self.pack()
        self.stworzWidgety()

    def stworzWidgety(self):
        gora=Frame(self)
        gora.pack()
        grupaSer=LabelFrame(gora,text=u"Połączenie",width=80, height =120)
        grupaSer.pack(side = LEFT)
        grupaSer.pack_propagate(0)
        przyciskPolacz = Button(grupaSer,text=u"Połącz",command=self.polacz)
        przyciskPolacz.pack()
        przyciskRozlacz = Button(grupaSer,text=u"Rozłącz",command=self.rozlacz)
        przyciskRozlacz.pack()

        grupaPorty=LabelFrame(gora,text=u"Lista Portów",width=320, height =120)
        grupaPorty.pack(side = RIGHT)
        grupaPorty.pack_propagate(0)
        self.listaPortow = Listbox(grupaPorty,width=35, height=5);
        self.listaPortow.pack()
        ports = list(serial.tools.list_ports.comports())
        for p in ports:
            self.listaPortow.insert(END,p)

        grupaPL=LabelFrame(self, text=u'Prawo/lewo',width=100, height =150)
        grupaPL.pack(side = LEFT)
        grupaPL.pack_propagate(0)
        suwakPL = Scale(grupaPL, from_=127, to=0, command=self.zmienPL)
        suwakPL.pack()
        przyciskPL = Button(grupaPL,text=u"Wyśrodkuj",command=self.wysrodkujPL) 
        przyciskPL.pack()

        grupaPT=LabelFrame(self, text=u'Przód/tył',width=100, height =150)
        grupaPT.pack(side = LEFT)
        grupaPT.pack_propagate(0)
        suwakPT = Scale(grupaPT, from_=127, to=0, command=self.zmienPT)
        suwakPT.pack()
        przyciskPT = Button(grupaPT,text=u"Wyśrodkuj",command=self.wysrodkujPT) 
        przyciskPT.pack()

        grupaW=LabelFrame(self, text='Wysokość',width=100, height =150)
        grupaW.pack(side = LEFT)
        grupaW.pack_propagate(0)
        suwakW = Scale(grupaW, from_=127, to=0, command=self.zmienW)
        suwakW.pack()
        przyciskW = Button(grupaW,text=u"Ląduj",command=self.wysrodkujW) 
        przyciskW.pack()

        grupaT=LabelFrame(self, text='Trym',width=100, height =150)
        grupaT.pack(side = LEFT)
        grupaT.pack_propagate(0)
        suwakT = Scale(grupaT, from_=127, to=0, command=self.zmienT)
        suwakT.pack()
        przyciskT = Button(grupaT,text=u"Wyśrodkuj",command=self.wysrodkujT) 
        przyciskT.pack()

        suwakPL.set(63)
        suwakPT.set(63)
        suwakW.set(0)
        suwakT.set(63)

    def polacz(self):
        port=str((self.listaPortow.get(ACTIVE)))
        if port[4]==" ":
            nport=port[:4]
        else:
            nport=port[:5]
        try:
            self.ser = serial.Serial(nport, 9600)
        except:
             print tkMessageBox.showinfo(u"Błąd",u"Nie można połączyć z wybranym portem")

    def rozlacz(self):
        if (self.ser.isOpen()):
            self.ser.close()

    def zmienPL(self, wartosc):
        if self.ser:
            self.wyslij(0,int(wartosc))

    def zmienPT(self, wartosc):
        if self.ser:
            self.wyslij(1,int(wartosc))

    def zmienW(self, wartosc):
        if self.ser:
            self.wyslij(2,int(wartosc))

    def zmienT(self, wartosc):
        if self.ser:
            self.wyslij(3,int(wartosc))

    def wysrodkujPL():
        suwakPL.set(63)

    def wysrodkujPT():
        suwakPT.set(63)

    def wysrodkujW():
        suwakW.set(0)

    def wysrodkujT():
        suwakT.set(63)

    def wyslij(self,pierwsza,druga):
        self.ser.write(chr(pierwsza))
        self.ser.write(chr(druga))

        


glowneOkno = Tk()
app = AplikacjaGUI(glowneOkno)
glowneOkno.mainloop()