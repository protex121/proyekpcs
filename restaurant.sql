DROP TABLE MEMBER CASCADE CONSTRAINTS PURGE;
DROP TABLE CABANG CASCADE CONSTRAINTS PURGE;
DROP TABLE JABATAN CASCADE CONSTRAINTS PURGE;
DROP TABLE ABSENSI CASCADE CONSTRAINTS PURGE;
DROP TABLE MENU CASCADE CONSTRAINTS PURGE;
DROP TABLE MENU_TENNANT CASCADE CONSTRAINTS PURGE;
DROP TABLE HTRANS CASCADE CONSTRAINTS PURGE;
DROP TABLE DTRANS CASCADE CONSTRAINTS PURGE;
DROP TABLE PEGAWAI CASCADE CONSTRAINTS PURGE;
DROP TABLE PROMO CASCADE CONSTRAINTS PURGE;
DROP TABLE TENNANT CASCADE CONSTRAINTS PURGE;


CREATE TABLE MEMBER(
	ID_MEMBER VARCHAR2(5) CONSTRAINT PK_MEMBER PRIMARY KEY,
	NAMA_MEMBER VARCHAR2(50),
	TGLLAHIR_MEMBER DATE,
	NOHP_MEMBER VARCHAR2(13)
);

CREATE TABLE CABANG(
	ID_CABANG VARCHAR2(3) CONSTRAINT PK_CABANG PRIMARY KEY,
	NAMA_CABANG VARCHAR2(50),
	ALAMAT_CABANG VARCHAR2(50),
	TELP_CABANG VARCHAR2(13)
);

CREATE TABLE JABATAN(
	ID_JABATAN VARCHAR2(2) CONSTRAINT PK_JABATAN PRIMARY KEY,
	NAMA_JABATAN VARCHAR2(16),
	GAJI_POKOK NUMBER(8)
);

CREATE TABLE PEGAWAI(
	ID_PEGAWAI VARCHAR2(5) CONSTRAINT PK_PEGAWAI PRIMARY KEY,
	NAMA_PEGAWAI VARCHAR2(50),
	PASS_PEGAWAI VARCHAR2(12),
	TGLLAHIR_PEGAWAI DATE,
	NOHP_PEGAWAI VARCHAR2(13),
	JABATAN_PEGAWAI VARCHAR2(2) CONSTRAINT FK_JABATAN_PEGAWAI REFERENCES JABATAN(ID_JABATAN),
	CABANG_PEGAWAI VARCHAR2(3) CONSTRAINT FK_CABANG_PEGAWAI REFERENCES CABANG(ID_CABANG)
);

CREATE TABLE ABSENSI(
	ID_PEGAWAI VARCHAR2(5) CONSTRAINT FK_ABSENSI_PEGAWAI REFERENCES PEGAWAI(ID_PEGAWAI),
	TGL_ABSENSI DATE,
	JAM_MASUK DATE,
	JAM_KELUAR DATE,
	CONSTRAINT PK_ABSENSI PRIMARY KEY (ID_PEGAWAI, TGL_ABSENSI)
);

CREATE TABLE TENNANT(
	ID_TENNANT VARCHAR2(7) CONSTRAINT PK_TENNANT PRIMARY KEY,
	NAMA_TENNANT VARCHAR2(30),
	ID_CABANG VARCHAR2(3) CONSTRAINT FK_ID_CABANG REFERENCES CABANG(ID_CABANG)
);

CREATE TABLE MENU(
	ID_MENU VARCHAR2(5) CONSTRAINT PK_MENU PRIMARY KEY,
	NAMA_MENU VARCHAR2(50),
	DESKRIPSI_MENU VARCHAR2(120) NULL
);

CREATE TABLE MENU_TENNANT(
	ID_MENU VARCHAR2(5) CONSTRAINT FK_MENU REFERENCES MENU(ID_MENU),
	ID_TENNANT VARCHAR2(7) CONSTRAINT FK_CABANG_MENU REFERENCES TENNANT(ID_TENNANT),
	HARGA_MENU NUMBER(7),
	CONSTRAINT PK_MENU_TENNANT PRIMARY KEY (ID_MENU, ID_TENNANT)
);

CREATE TABLE HTRANS(
	ID_TRANS VARCHAR2(15) CONSTRAINT PK_TRANS PRIMARY KEY,
	WAKTU_TRANS DATE,
	TOTAL_TRANS NUMBER(10),
	ID_PROMO VARCHAR2(5),
	ID_MEMBER VARCHAR2(5),
	ID_KASIR VARCHAR2(5) CONSTRAINT FK_PEGAWAI_TRANS REFERENCES PEGAWAI(ID_PEGAWAI)
);

CREATE TABLE DTRANS(
	ID_TRANS VARCHAR2(15) CONSTRAINT FK_TRANS REFERENCES HTRANS(ID_TRANS),
	ID_MENU VARCHAR2(5) CONSTRAINT FK_MENU_DTRANS REFERENCES MENU(ID_MENU),
	JML_MENU NUMBER(3),
	HARGA_MENU NUMBER(7),
	CONSTRAINT PK_DTRANS PRIMARY KEY (ID_TRANS, ID_MENU)
);

CREATE TABLE PROMO(
	ID_PROMO VARCHAR2(5) CONSTRAINT PK_PROMO PRIMARY KEY,
	TITLE_PROMO VARCHAR2(30),
	JML_PROMO NUMBER (6)
);

INSERT INTO MEMBER VALUES('GL001','George Lin',to_date('12/3/1998','dd/mm/yyyy'),'081222638187');
INSERT INTO MEMBER VALUES('IC001','Ian Clarkson',to_date('26/1/1995','dd/mm/yyyy'),'081328491642');
INSERT INTO MEMBER VALUES('JB001','Jordan Bernard',to_date('01/8/1999','dd/mm/yyyy'),'081482361835');
INSERT INTO MEMBER VALUES('UM001','Ursula Marie',to_date('08/7/1998','dd/mm/yyyy'),'081628402712');
INSERT INTO MEMBER VALUES('LI001','Lindt',to_date('19/11/1997','dd/mm/yyyy'),'081124174936');
INSERT INTO MEMBER VALUES('MV001','Mario Venere',to_date('20/09/2000','dd/mm/yyyy'),'081648938165');
INSERT INTO MEMBER VALUES('GL002','Glenn',to_date('17/7/1997','dd/mm/yyyy'),'081773625389');

INSERT INTO CABANG VALUES('T01','TUNJUNGAN PLAZA','JL. JENDERAL BASUKI RACHMAT','081224556988');

INSERT INTO JABATAN VALUES('M1','MANAGER',30000000);
INSERT INTO JABATAN VALUES('K1','KOKI',12000500);
INSERT INTO JABATAN VALUES('M2','MARKETING',10800000);
INSERT INTO JABATAN VALUES('B1','BARTENDER',9700000);
INSERT INTO JABATAN VALUES('K2','KASIR',3100000);
INSERT INTO JABATAN VALUES('P1','PELAYAN',3400000);
INSERT INTO JABATAN VALUES('C1','CLEANING SERVICE',2300000);

INSERT INTO PEGAWAI VALUES('NR001','Nat Reinard', 'natrei1',to_date('25/9/1991','dd/mm/yyyy'),'081846298165','M1','T01');
INSERT INTO PEGAWAI VALUES('HR001','Heron Reese', 'hrhero',to_date('18/7/1994','dd/mm/yyyy'),'081736251947','M2','T01');
INSERT INTO PEGAWAI VALUES('BT001','Bree Taylor', 'breeezey',to_date('05/6/1997','dd/mm/yyyy'),'081645353777','M2','T01');
INSERT INTO PEGAWAI VALUES('JK001','Jeremiah Kennard', 'miahthebar',to_date('13/12/1998','dd/mm/yyyy'),'081777888625','B1','T01');
INSERT INTO PEGAWAI VALUES('NN001','Nerlens Nivuna', 'doublen2',to_date('19/1/1998','dd/mm/yyyy'),'081827198223','K1','T01');
INSERT INTO PEGAWAI VALUES('PR001','Priya Raachak', 'priya555',to_date('11/3/1998','dd/mm/yyyy'),'081009874986','K1','T01');
INSERT INTO PEGAWAI VALUES('RD001','Rezno Dmitriy', 'reztherus',to_date('16/4/1999','dd/mm/yyyy'),'081778465321','K1','T01');
INSERT INTO PEGAWAI VALUES('PD001','Pablo DiMaria', 'pabs96',to_date('19/8/1996','dd/mm/yyyy'),'081253618726','K2','T01');
INSERT INTO PEGAWAI VALUES('JH001','Juan Hernandez', 'juanher001',to_date('09/1/1999','dd/mm/yyyy'),'081892054015','P1','T01');
INSERT INTO PEGAWAI VALUES('JO001','Joe', 'itsjustjoe',to_date('24/5/1998','dd/mm/yyyy'),'081097489501','P1','T01');
INSERT INTO PEGAWAI VALUES('KR001','Klein Rufus', 'cleanklein',to_date('26/11/1997','dd/mm/yyyy'),'081876382372','P1','T01');
INSERT INTO PEGAWAI VALUES('BS001','Bambang Satriyo', 'bambambang',to_date('1/12/1999','dd/mm/yyyy'),'081602904713','C1','T01');
INSERT INTO PEGAWAI VALUES('MK001','Mahatir Khana', 'indianhwer',to_date('23/2/1998','dd/mm/yyyy'),'081293510573','C1','T01');

INSERT INTO ABSENSI VALUES();

INSERT INTO TENNANT VALUES('PR01T01','PEPSI RESERVOIR','T01');
INSERT INTO TENNANT VALUES('NG01T01','NGOCOL','T01');
INSERT INTO TENNANT VALUES('PJ01T01','PANGSIT MIE JAGARAGA','T01');
INSERT INTO TENNANT VALUES('KF01T01','KFC','T01');
INSERT INTO TENNANT VALUES('RS01T01','RM SEDERHANA','T01');
INSERT INTO TENNANT VALUES('AM01T01','AYAM GORENG MAMA','T01');

INSERT INTO MENU VALUES('PE001','Pepsi','Segelas pepsi');
INSERT INTO MENU VALUES('CO001','Coca-Cola','Segelas coca-cola');
INSERT INTO MENU VALUES('DO001','Doritos','Sebungkus doritos');
INSERT INTO MENU VALUES('AM001','Air mineral','Segelas air mineral');
INSERT INTO MENU VALUES('FF001','French Fries','Semangkuk penuh french fries sebagai pelengkap hidangan utama');
INSERT INTO MENU VALUES('BA001','Batagor','Campuran tahu dan kentang yang dilumuri dengan tepung kemudian digoreng untuk menambah kelezatan');
INSERT INTO MENU VALUES('NA001','Nasi Ayam Goreng','Nikmati kelezatan ayam goreng KFC dengan menu yang tersimpel');
INSERT INTO MENU VALUES('BA002','Burger Ayam','Burger dengan isian ayam goreng khas KFC');
INSERT INTO MENU VALUES('SB001','Spaghetti Bolognese','Spaghetti dengan bumbu yang asli berasal dari italia');
INSERT INTO MENU VALUES('NG001','Nasi Goreng Ayam','Nasi goreng dengan ayam yang diracik secara spesial');
INSERT INTO MENU VALUES('NG002','Nasi Goreng Sosis','Nasi goreng dengan sosis sapi olahan Ngocol');
INSERT INTO MENU VALUES('NR001','Nasi Rendang','Nikmati hidangan terlezat di dunia');
INSERT INTO MENU VALUES('NA002','Nasi Ayam Goreng Mama','Nikmati kelezatan ayam goreng resep asli Mama');

INSERT INTO MENU_TENNANT VALUES('PE001','PR01T01',5000);
INSERT INTO MENU_TENNANT VALUES('CO001','PR01T01',5000);
INSERT INTO MENU_TENNANT VALUES('DO001','PR01T01',11000);
INSERT INTO MENU_TENNANT VALUES('AM001','PR01T01',3000);
INSERT INTO MENU_TENNANT VALUES('FF001','KF01T01',10000);
INSERT INTO MENU_TENNANT VALUES('BA001','PR01T01',9000);
INSERT INTO MENU_TENNANT VALUES('NA001','KF01T01',22000);
INSERT INTO MENU_TENNANT VALUES('BA002','KF01T01',18000);
INSERT INTO MENU_TENNANT VALUES('SB001','KF01T01',16000);
INSERT INTO MENU_TENNANT VALUES('NG001','NG01T01',15000);
INSERT INTO MENU_TENNANT VALUES('NG002','NG01T01',12500);
INSERT INTO MENU_TENNANT VALUES('NR001','RS01T01',20000);
INSERT INTO MENU_TENNANT VALUES('NA002','AM01T01',21000);


