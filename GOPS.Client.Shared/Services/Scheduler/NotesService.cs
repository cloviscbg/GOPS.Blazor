namespace GOPS.Client.Shared.Services;

using System.Collections.Generic;

using GOPS.Client.Shared.Extensions;
using GOPS.Client.Shared.Models;


public class NotesService : INotesService
{
	public void Add(Note note)
	{
		var existNote = GetByDate(note.Date);

		if (existNote is not null) 
		{
			return;
		}

		SeederExtensions.Notes.Add(note);
	}

	public void Delete(Guid id)
	{
		var existNote = GetById(id);

		if (existNote is null)
		{
			return;
		}

		SeederExtensions.Notes.Remove(existNote);
	}

	public Note? GetByDate(DateTime date)
	{
		return SeederExtensions.Notes.SingleOrDefault(x => x.Date.IsDateEqual(date));
	}

	public IEnumerable<Note> GetByDates(DateTime start, DateTime end)
	{
		return SeederExtensions.Notes.Where(x => x.Date.IsBetween(start, end));
	}

	public Note? GetById(Guid id)
	{
		return SeederExtensions.Notes.SingleOrDefault(x => x.Id == id);
	}

	public IEnumerable<Note> Get(DateTime date)
	{
		return SeederExtensions.Notes.Where(x => x.Date.Month == date.Month);
	}

	public void Update(Note note)
	{
		var existNote = GetByDate(note.Date);

		if (existNote is not null)
		{
			existNote.Text = note.Text;
			return;
		}

		Add(note);
	}
}
