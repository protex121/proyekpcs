CREATE OR REPLACE FUNCTION AUTO_GEN_ID_KASIR( 
	nama varchar2
)
return varchar2
IS
	idx number;
	idxspasi number;
	tkode varchar2(5);
	tempkode varchar2(2);
BEGIN
	idxspasi := instr(nama,' ',-1);
	tempkode := UPPER(substr(nama,1,2));
	idx := 1;
	FOR i IN (SELECT substr(p.id_pegawai,1,2) as "KODE" FROM pegawai p GROUP BY p.id_pegawai) LOOP
		IF tempkode = i.KODE THEN
			idx := idx + 1;
		END IF;
	END LOOP;
	
	IF idxspasi = 0 THEN
		tkode := SUBSTR(nama,1,1) || UPPER(SUBSTR(nama,2,1))|| lpad(idx,3,'0');
	ELSE
		tkode := SUBSTR(nama,1,1) || SUBSTR(nama,idxspasi+1,1)|| lpad(idx+1,3,'0');
	END IF;
	
	return tkode;
END;
/
SHOW ERR;