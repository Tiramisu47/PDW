export const helper = {
  getRandomInt(minInclusive: number, maxExclusive: number): number {
    const minCeiled = Math.ceil(minInclusive);
    const maxFloored = Math.floor(maxExclusive);
    return Math.floor(Math.random() * (maxFloored - minCeiled) + minCeiled);
  },
};
