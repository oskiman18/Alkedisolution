use  Alkemy

go

insert into Genero(imagen,nombre)
values	('https://tinyurl.com/yc2458ka', 'Musical'),
		('https://tinyurl.com/3cvfmwwv','Fantasia'),
		('https://tinyurl.com/4b822m47','Accion'),
		('https://tinyurl.com/3fpya9dp','Historia'),
		('https://tinyurl.com/4temvs5p','Western')
go

insert into Personaje(nombre,edad,peso,historia,imagen)
values	('Mickey', 34, 40.134,'El malevolo lider de una organizacion que poco a poco dominara el mundo', 'https://tinyurl.com/2p98eyva'),
		('Goofy', 43, 58.201, 'Padre soltero y mejor amigo de mickey, siempre sonrie, un crack el pibe','https://tinyurl.com/msukwrs2'),
		('Donald',38, 52.100, 'Un pato con 3 sobrinos y 1 primo millonario, habla mucho pero nunca se le entiende','https://tinyurl.com/3445zeu6'),
		('Minnie',31, 35.154 ,'Esposa de Mickey, nunca vi la vi participar tanto en las peliculas','https://tinyurl.com/vn9xs6w6'),
		('Pluto', 10, 25.675, 'Mascota de Mickey, es un perro como el mejor amigo de su dueño, solo que el no sabe hablar','https://tinyurl.com/axz79ddk')

go

insert into Pelicula(titulo,fCreacion,calif,imagen,GeneroId)
values	('Fantasia: La pelicula', '2004-05-20', 4, 'https://tinyurl.com/2p8ru7p2', 2),
		('Los 3 mosqueteros', '1940-01-18', 5, 'https://tinyurl.com/j5tnpufe', 4),
		('Minnie-rella', '2014-03-08' , 3,'https://tinyurl.com/2p9hcpmn',1)

go

insert into PeliculaPersonaje(personajesId,peliculasid)
values	(1,1),(1,2),(1,3),(2,2),(3,1),(3,2),(4,1),(4,3)