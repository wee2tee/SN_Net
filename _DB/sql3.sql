Select S.id as id, S.sernum as sernum,S.dealer_id as dealer_id, D.dealercod as dealercod, S.flag as flag From (Select id, dealercod From dealer Where flag=0) D Right Join serial S On D.id = S.dealer_id Having flag = 0 Order By dealercod ASC, sernum ASC