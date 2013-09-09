k94warriors
===========

Schema:
------

http://i.imgur.com/aXWwy3y.png

Requirements:
------------

Core Requirements:

	* Must have some sort of alerting - Immediate SMS?  Immediate email?  Daily Report? 
	* Must be able to create dog records based on initial evaluation of dog
	* 
		* Positive or negative - take dog or pass

	* Must be able to track dog records for daily facilities - Volunteers
	* 
		* Types of notes -
		* 
			* Daily Status Note
			* Critical Note


	* Must be able to track Reminders on the dog records
	* 
		* Vets
		* Other actions that need to happen

	* Must be able to track Commands 
	* 
		* Same as notes

	* Must be able to track dog training records - Trainers
	* Must be able to track dogs to warriors
	* Must be able to track feeding plan and records
	* 
		* Feedings would be notes
		* Dog Feeding text associated with dog

	* Must be able to track vet records by Dogs
	* 
		* Associate image to the vet record
		* Track shot history

	* Must be able to track graduation and re-certification at specific timeframes?
	* Must be able to track location of dog inside kennel 
	* Must be able to track kennel cleaning/operations 
	* Must be able to support user types:
	* 
		* Ability to add new users



Secondary Requirements:

	* Tracking Dogs to Sponsors
	* Warriors able to update statuses / photos of dogs after?
	* 
		* Tie into social?

	* Ability to track history of shelter information

		* Ability to have different types of users 
		* 
			* Nice to have - Types of Users:
			* 
				* Volunteers
				* Trainers
				* Dog Evaluators
				* Administrative
				* Warriors/Veterns





High Level Models:

	* Dog:
	* 
		* Dog Properties:
		* 
			* Source - Where the dog came from?  Shelter/home/etc?
			* 
				* Sources database
				* Associated to types in the Sources database

			* Birthdate?  (Age?)
			* Breed
			* Color
			* Photos
			* Spaid/Neutered date
			* Current Temperament - Aggression
			* Energy Level
			* Current Medical Issues
			* Date Acquired
			* Date Graduated
			* Warrior Adopted to
			* Re certification Due Date
			* Name (Optional)
			* ID (Required)

		* Training Log
		* 
			* General - Free Text

		* Daily Status Updates
		* 
			* General - Free Text
			* Temperament Updates
			* Alerts

		* Vet Records
		* 
			* General - Free Text / File Upload
			* Structured - Shots
			* Medical History

		* Feeding Schedule
		* Feeding Event
		* Interview Contact - Person who did the interview
		* Skills 
		* Warrior Follow Up / Recertification

	* User: 
	* Kennel:
	* Warriors:
	* Dog Source:
	* 
		* Shelter
		* Individual
		* Organization
		* Notes on the source



