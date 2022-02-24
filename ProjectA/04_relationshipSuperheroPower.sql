USE SuperherosDb

CREATE TABLE SuperheroPowers(
	SuperheroId int FOREIGN KEY REFERENCES Superhero(Id),
	PowerId int FOREIGN KEY REFERENCES Power(Id)
)
