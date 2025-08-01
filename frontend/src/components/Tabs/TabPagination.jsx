/* eslint-disable react/prop-types */
function TabPagination({ qtd, actualPage, onPageChange }) {
  const pages = Math.ceil(qtd / 64);
  const pageNumbers = Array.from({ length: pages }, (_, i) => i + 1);

  return (
    <div className="flex gap-2 justify-start mt-4">
      {pageNumbers.map((n, index) => {
        return (
          <button
            key={index}
            onClick={() => onPageChange(n)}
            className={`px-3 py-1 rounded border ${
              n === actualPage
                ? "bg-blue-600 text-white font-bold"
                : "bg-gray-200 text-gray-800"
            }`}
          >
            {n}
          </button>
        );
      })}
    </div>
  );
}

export default TabPagination;
