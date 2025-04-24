
type SectionProps = {
    title: string;
    items: { imageSrc: string; text: string }[];
    isAdmin?: boolean;
  };

function Section({ title, items, isAdmin = false }: SectionProps) {
    return (
        <div className="my-10">
          {/* Title & Add Button */}
          <div className="flex justify-between items-center mb-4">
            <h2 className="text-2xl font-bold">{title}</h2>
            {isAdmin && (
              <button className="bg-gray-300 px-3 py-1 text-sm rounded hover:bg-gray-200">
                Add
              </button>
            )}
          </div>
    
          {/* Scrollable Section */}
          <div className="relative bg-gray-200 p-4">
            <div className="flex gap-4 overflow-x-auto scrollbar-hide">
              {items.map((item, index) => (
                <div key={index} className="min-w-[250px] bg-gray-800 text-white p-3 rounded">
                  <img
                    src={item.imageSrc}
                    alt={`item-${index}`}
                    className="w-full h-32 object-cover mb-2"
                  />
                  <p>{item.text}</p>
                  {isAdmin && (
                    <div className="flex justify-between mt-2">
                      <button className="bg-gray-600 px-3 py-1 rounded hover:bg-gray-500">
                        Edit
                      </button>
                      <button className="bg-gray-600 px-3 py-1 rounded hover:bg-red-500">
                        Delete
                      </button>
                    </div>
                  )}
                </div>
              ))}
            </div>
          </div>
        </div>
      );
}

export default Section;