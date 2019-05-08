CREATE OR REPLACE FUNCTION AUTO_GEN_ID_CABANG(nama_cabang varchar2
)

return varchar2

IS
	
	idx number;
	
	tkode varchar2(3);
	
	tempkode varchar2(3);

BEGIN
	
	tempkode := UPPER(substr(nama_cabang,0,1));

	idx := 1;
	
	FOR i IN (SELECT substr(c.id_cabang,1,1) as "KODE" FROM cabang c GROUP BY c.id_cabang) LOOP
		
		IF tempkode = i.KODE THEN
			
			idx := idx + 1;
		
		END IF;
	END LOOP;
	
	
	tkode := UPPER(substr(nama_cabang,0,1)) || lpad(idx,2,'0');
	
	return tkode;

END;

/

SHOW ERR;