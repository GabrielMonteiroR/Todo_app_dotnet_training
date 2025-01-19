namespace TodoApp.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly IMapper _mapper;

        public TodoController(TodoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoReadDto>>> GetTodoItems()
        {
            var todoItems = await _context.TodoItems.ToListAsync();
            if (todoItems is null) return HttpException.NotFound();

            return Ok(_mapper.Map<IEnumerable<TodoReadDto>>(todoItems));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoReadDto>> GetTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem is null) return HttpException.NotFound();

            return Ok(_mapper.Map<TodoReadDto>(todoItem));
        }

        [HttpPost]
        public async Task<ActionResult<TodoReadDto>> CreateTodoItem(TodoCreateUpdateDto todoCreateUpdateDto)
        {
            var todoItem = _mapper.Map<TodoItem>(todoCreateUpdateDto);
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, _mapper.Map<TodoReadDto>(todoItem));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoReadDto>> UpdateTodoItem(int id, TodoCreateUpdateDto todoCreateUpdateDto)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem is null) return HttpException.NotFound();

            _mapper.Map(todoCreateUpdateDto, todoItem);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<TodoReadDto>(todoItem));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem is null) return HttpException.NotFound();

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}