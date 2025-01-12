namespace GOPS.Client.Shared.Services;

using GOPS.Client.Shared.Models;

public interface INotesService
{
	public IEnumerable<Note> Get(DateTime date);
	public IEnumerable<Note> GetByDates(DateTime start, DateTime end);
	public Note? GetById(Guid id);
	public Note? GetByDate(DateTime date);
	public void Add(Note note);
	public void Update(Note note);
	public void Delete(Guid id);
}
